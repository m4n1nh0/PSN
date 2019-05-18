using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using PSN2018.Models;
using System.IO;

namespace PSN2018.Controllers
{
    public class LoginsController : Controller
    {
        private PSN2018Context db = new PSN2018Context();
        private Criptografia cripto = new Criptografia();

        // GET: Logins
        public ActionResult Index()
        {
            return View(db.Logins.ToList());
        }

        // GET: Logins/Perfils/5
        public ActionResult Perfil()
        {
            string id = Session["id"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(login);
        }

        // GET: Logins/Imagem/5
        public ActionResult Imagem(string strCPFCNPJ)
        {
            if (strCPFCNPJ == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            strCPFCNPJ = strCPFCNPJ.Replace(".","").Replace("-","");
            var pastaDeImagens = Server.MapPath("~/content/imagens");
            var caminhoCompleto = Path.Combine(pastaDeImagens, Path.GetFileName(strCPFCNPJ));
            try
            {
                Login login = db.Logins.Where(lg => lg.strCPFCNPJ == strCPFCNPJ).First();
                if (login.ImageUrl != null)
                {
                    FileInfo file = new FileInfo(login.ImageUrl);
                    if (file.Exists)
                    {
                        caminhoCompleto = login.ImageUrl;
                    }
                    else
                    {
                        caminhoCompleto = Path.Combine(pastaDeImagens, Path.GetFileName("user.png"));
                    }
                      
                }
                else
                {
                    caminhoCompleto = Path.Combine(pastaDeImagens, Path.GetFileName("user.png"));
                }
            }
            catch
            {
                caminhoCompleto = Path.Combine(pastaDeImagens, Path.GetFileName("user.png"));
            }
            return File(caminhoCompleto, Path.GetExtension(caminhoCompleto), "perfil");
        }

        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,strCPFCNPJ,strSenha,intNivel,ImageUrl")] Login login, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] {".Jpg", ".png", ".jpg", "jpeg"}; //extencoes permitidas
            var fileName = Path.GetFileName(file.FileName); //pegando nome do arquivo
            var ext = Path.GetExtension(file.FileName); //pegando extensao do arquivo
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); // pegando nome do arquivo sem a extencao
                string myfile = login.strCPFCNPJ + ext; //anexando o nome da imagem ao cpf/cnpj
                var path = Path.Combine(Server.MapPath("~/Content/imagens/"), myfile);//armazenando o arquivo dentro da pasta ~/Content/imagens
                string shcript = cripto.getMD5Hash(login.strSenha);
                login.strSenha = shcript;
                login.ImageUrl = path;
                if (ModelState.IsValid)
                {
                    db.Logins.Add(login);
                    db.SaveChanges();
                    file.SaveAs(path);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.message = "Por favor selecione uma imagem valida!";
            }


            return View(login);
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,strCPFCNPJ,strSenha,intNivel")] Login login)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(login.strSenha))
                {
                    string shcript = cripto.getMD5Hash(login.strSenha);
                    login.strSenha = shcript;
                }
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
