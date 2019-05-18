using PSN2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PSN2018.Controllers
{
    public class AutenticationController : Controller
    {
        private PSN2018Context db = new PSN2018Context();
        private Criptografia cripto = new Criptografia();

        //Aqui começo a funcoes de login
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["User"];
            Login login = new Login();
            if (cookie != null)
            {
                string[] valores = cookie.Value.ToString().Split('&');
                login.strCPFCNPJ = valores[0];
            }
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login objUser, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var obj = db.Logins.Where(a => a.strCPFCNPJ.Equals(objUser.strCPFCNPJ)).FirstOrDefault(); //&& a.strSenha.Equals(objUser.strSenha)
                if (obj != null)
                {
                    string shcript = cripto.getMD5Hash(objUser.strSenha);
                    if (obj.strSenha.Equals(shcript))
                    {
                        Session["LoginID"] = obj.id.ToString();
                        Session["LoginCPFCNPJ"] = obj.strCPFCNPJ.ToString();
                        Session["LoginNOME"] = obj.strNome.ToString();
                        Session["LoginFUNCA"] = this.Funcao(obj.strCPFCNPJ.ToString());
                        if (!obj.strCPFCNPJ.Equals(null))
                        {
                            Session["LoginCPFCNPJ"] = obj.strCPFCNPJ.ToString();
                        }
                        else
                        {
                            Session["LoginCPFCNPJ"] = "Nome não cadastrado";
                        }
                        bool remenber = (form["remenber"] ?? "").Equals("on", StringComparison.CurrentCultureIgnoreCase);
                        if (remenber)
                        {
                            FormsAuthentication.SignOut();
                            FormsAuthentication.SetAuthCookie(obj.strCPFCNPJ, remenber);
                            HttpCookie cookie = Request.Cookies["User"];
                            if (cookie == null)
                            {
                                // Criando a Instância do cookie
                                cookie = new HttpCookie("User",obj.strCPFCNPJ);
                                //Adicionando a propriedade "Usuario" no cookie
                                cookie.Values.Add("usuario", obj.strCPFCNPJ);
                                //colocando o cookie para expirar em 365 dias
                                cookie.Expires = DateTime.Now.AddHours(1);
                                // Definindo a segurança do nosso cookie
                                cookie.HttpOnly = true;
                                // Registrando cookie
                                this.Response.AppendCookie(cookie);
                            }else
                            {
                                Response.Cookies["User"].Expires = DateTime.Now.AddHours(-1);
                            }
                        }else
                        {
                            Response.Cookies["User"].Expires = DateTime.Now.AddHours(-1);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Senha Inválida!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário Inválido!");
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["LoginID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Autentication");
            }
        }

        public ActionResult Logout()
        {

            Session["LoginID"] = null;
            Session["LoginCPFCNPJ"] = null;
            Session["LoginNOME"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Autentication");
        }

        public ActionResult Funcoes()
        {
            String funcao = Funcao(Session["LoginCPFCNPJ"].ToString());
            return Content(funcao);
        }


        public String Funcao(String strCPFCNPJ)
        {
            var func = db.Funcionarios.Where(a => a.strCPF.Equals(strCPFCNPJ)).FirstOrDefault(); //&& a.strSenha.Equals(objUser.strSenha)
            if (func != null)
            {
                var funcao = db.Funcaos.Where(a => a.id.Equals(func.intFuncaoID_FK)).FirstOrDefault();
                if (funcao != null)
                {
                    return funcao.strDsc;
                }
                else
                {
                    return func.intFuncaoID_FK+"-Funcão Não Cadastrada";
                }
            }
            else
            {
                var cli = db.Clientes.Where(a => a.strCPFCNPJ.Equals(strCPFCNPJ)).FirstOrDefault();
                if (cli != null)
                {
                    return "Cliente";
                }
                else
                {
                    var gerproj = db.GerenteProjetoes.Where(a => a.strCPF.Equals(strCPFCNPJ)).FirstOrDefault();
                    if (gerproj != null)
                    {
                        return "Gerente de Projetos";
                    }
                }
            }
            return "Função Não Localizada";
        }
    }
}