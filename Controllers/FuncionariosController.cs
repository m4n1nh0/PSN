using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSN2018.Models;

namespace PSN2018.Controllers
{
    public class FuncionariosController : Controller
    {
        private PSN2018Context db = new PSN2018Context();
        public listFuncSkill listFuncSkill = new listFuncSkill();

        public class FuncPerfil
        {
            public int id { get; set; }
            public string strCPF { get; set; }
            public string strNome { get; set; }        
            public double dblProdutiv { get; set; }
            public int intAtividades { get; set; }
            public string strFuncao { get; set; }
            public string url_imagem { get; set; }
        }

        // GET: Funcionarios
        public ActionResult Index()
        {

            List<Funcionario> list = new List<Funcionario>();
            var funci = db.Funcionarios.ToList();
            foreach (var f in funci)
            {
                Funcionario fun = new Funcionario();
                Funcao funcao = db.Funcaos.Find(f.intFuncaoID_FK);
                fun = f;
                fun.strFuncao = funcao.strDsc;
                list.Add(fun);
            }

            return View(list);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            ViewBag.Funcoes = db.Funcaos.ToList();
            ViewBag.Skill = db.Skills.ToList();
            if (listFuncSkill.listSkill == null)
            {
                listFuncSkill.listSkill = new List<FuncSkill>();
            }

            ViewBag.FuncSkill = listFuncSkill.listSkill;
            Session["data"] = listFuncSkill.listSkill;
            return View();
        }

        // POST: Funcionarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,strCPF,strNome,dblProdutiv,intAtividades,intFuncaoID_FK")] Funcionario funcionario, string Lista_Skill, string Nome_Skill, string Nivel, string BtnAdd, string BtnRem, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (BtnAdd == null && BtnRem == null) {
                    db.Funcionarios.Add(funcionario);
                    db.SaveChanges();
                    listFuncSkill.listSkill = (List<FuncSkill>)Session["data"];
                    foreach (FuncSkill fskil in listFuncSkill.listSkill)
                    {
                        SkillFuncionarios skllfunc = new SkillFuncionarios();
                        skllfunc.intFuncID_FK = funcionario.id;
                        skllfunc.intSkillID_FK = fskil.id;
                        skllfunc.decPerc = fskil.decPerc;                    
                        db.SkillFuncionarios.Add(skllfunc);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    if (BtnAdd != null)
                    {
                        FuncSkill funcSkill = new FuncSkill();
                        Skill skill = new Skill();
                        listFuncSkill.listSkill = (List<FuncSkill>)Session["data"];
                        if (String.IsNullOrEmpty(Lista_Skill))
                        {
                            funcSkill.id = 0;
                        }
                        else
                        {
                            funcSkill.id = int.Parse(Lista_Skill);
                        }

                        if (Nivel == null)
                        {
                            Nivel = "0,00";
                        }

                        funcSkill.decPerc = decimal.Parse(Nivel);

                        if (funcSkill.id > 0)
                        {
                            skill = db.Skills.Find(funcSkill.id);
                            funcSkill.strskill = skill.strDsc;
                            listFuncSkill.listSkill.Add(funcSkill);
                        }
                        else
                        {
                            funcSkill.strskill = Nome_Skill;
                            skill = db.Skills.Where(c => c.strDsc == Nome_Skill).FirstOrDefault();
                            if (skill == null)
                            {
                                skill = new Skill();
                                skill.strDsc = funcSkill.strskill;
                                db.Skills.Add(skill);
                                db.SaveChanges();
                            }
                            funcSkill.id = skill.id;
                            listFuncSkill.listSkill.Add(funcSkill);
                        }
                    }
                    else
                    {
                        if (BtnRem != null)
                        {
                            listFuncSkill.listSkill = (List<FuncSkill>)Session["data"];
                            string[] AllStrings = form["fskill"].Split(',');
                            foreach (string item in AllStrings)
                            {
                                int value = int.Parse(item);
                                var itemList = listFuncSkill.listSkill.First(x => x.id == value);
                                listFuncSkill.listSkill.Remove(itemList);
                            }
                            //var allvalues = form["fskill"].Split(',').Select(x => int.Parse(x));
                        }
                    }
                }
            }

            Session["data"] = listFuncSkill.listSkill;
            ViewBag.Funcoes = db.Funcaos.ToList();
            ViewBag.FuncSkill = listFuncSkill.listSkill;
            ViewBag.Skill = db.Skills.ToList();

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,strCPF,strNome,dblProdutiv,intAtividades,intFuncaoID_FK")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);
            db.Funcionarios.Remove(funcionario);
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

        //public ActionResult Remover(int skillFunc, string CPF, string Nome, int Quant, int Funcao)
        //{
        //    Funcionario funcionario = new Funcionario();
        //    funcionario.strCPF = CPF;
        //    funcionario.intAtividades = Quant;
        //    funcionario.strNome = Nome;
        //    funcionario.intFuncaoID_FK = Funcao;

        //    if (skillFunc == 0)
        //    {
        //        ModelState.AddModelError("", "Falha na Remoção, Conctar o Suporte!");
        //        return View("Create", funcionario);
        //    }
        //    listFuncSkill.listSkill = (List<FuncSkill>)Session["data"];
        //    var item = listFuncSkill.listSkill.First(x => x.id == skillFunc);
        //    listFuncSkill.listSkill.Remove(item);
        //    Session["data"] = listFuncSkill.listSkill;
        //    ViewBag.Funcoes = db.Funcaos.ToList();
        //    ViewBag.FuncSkill = listFuncSkill.listSkill;
        //    ViewBag.Skill = db.Skills.ToList();

        //    return View(funcionario);
        //}

    }
}
