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
    public class EquiepeFuncionariosController : Controller
    {
        private PSN2018Context db = new PSN2018Context();
        public listEquipeFunc ListEquipeFunc = new listEquipeFunc();

        // GET: EquiepeFuncionarios
        public ActionResult Index()
        {
            return View(db.EquiepeFuncionarios.ToList());
        }

        // GET: EquiepeFuncionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquiepeFuncionarios equiepeFuncionarios = db.EquiepeFuncionarios.Find(id);
            if (equiepeFuncionarios == null)
            {
                return HttpNotFound();
            }
            return View(equiepeFuncionarios);
        }

        // GET: EquiepeFuncionarios/Create
        public ActionResult Create()
        {
            if (ListEquipeFunc.listEquipe == null)
            {
                ListEquipeFunc.listEquipe = new List<FunciEquipe>();
            }
            ViewBag.Equipe = db.Equipes.ToList();
            ViewBag.Funcionario = db.Funcionarios.ToList();
            ViewBag.EquipeFunc = ListEquipeFunc.listEquipe;
            Session["data"] = ListEquipeFunc.listEquipe;
            return View();
        }

        // POST: EquiepeFuncionarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,intEquipeID_FK,intFuncID_FK,strTipo,intStatus")] EquiepeFuncionarios equiepeFuncionarios, string BtnAdd, string BtnRem, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (BtnAdd == null && BtnRem == null)
                {
                    ListEquipeFunc.listEquipe = (List<FunciEquipe>)Session["data"];

                    if (ListEquipeFunc.listEquipe.Count > 0)
                    {
                        foreach (var fequipe in ListEquipeFunc.listEquipe)
                        {
                            equiepeFuncionarios = new EquiepeFuncionarios();
                            equiepeFuncionarios.intEquipeID_FK = fequipe.intEquipeID_FK;
                            equiepeFuncionarios.intFuncID_FK = fequipe.intFuncID_FK;
                            equiepeFuncionarios.intStatus = fequipe.intStatus;
                            equiepeFuncionarios.strTipo = fequipe.strTipo;
                            db.EquiepeFuncionarios.Add(equiepeFuncionarios);
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Nenhum Funcionario Adicionado na Equipe!");
                    }
                }
                else
                {
                    if (BtnAdd != null)
                    {
                        FunciEquipe funciEquipe = new FunciEquipe();
                        funciEquipe.intFuncID_FK = equiepeFuncionarios.intFuncID_FK;
                        funciEquipe.intEquipeID_FK = equiepeFuncionarios.intEquipeID_FK;
                        Funcionario funcionario = db.Funcionarios.Find(equiepeFuncionarios.intFuncID_FK);
                        funciEquipe.strNomeFunc = funcionario.strNome;
                        Equipe equipe = db.Equipes.Find(equiepeFuncionarios.intEquipeID_FK);
                        funciEquipe.strNomeEquipe = equipe.strDsc;
                        funciEquipe.strTipo = equiepeFuncionarios.strTipo;
                        funciEquipe.intStatus = equiepeFuncionarios.intStatus;
                        ListEquipeFunc.listEquipe = (List<FunciEquipe>)Session["data"];
                        ListEquipeFunc.listEquipe.Add(funciEquipe);
                    }
                    else
                    {
                        if (BtnRem != null)
                        {
                            ListEquipeFunc.listEquipe = (List<FunciEquipe>)Session["data"];
                            string[] AllStrings = form["fEqui"].Split(',');
                            foreach (string item in AllStrings)
                            {
                                int value = int.Parse(item);
                                var itemList = ListEquipeFunc.listEquipe.First(x => x.intFuncID_FK == value);
                                ListEquipeFunc.listEquipe.Remove(itemList);
                            }

                        }
                    }
                }

            }

            ViewBag.Equipe = db.Equipes.ToList();
            ViewBag.Funcionario = db.Funcionarios.ToList();
            ViewBag.EquipeFunc = ListEquipeFunc.listEquipe;
            Session["data"] = ListEquipeFunc.listEquipe;

            return View(equiepeFuncionarios);
        }

        // GET: EquiepeFuncionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquiepeFuncionarios equiepeFuncionarios = db.EquiepeFuncionarios.Find(id);
            if (equiepeFuncionarios == null)
            {
                return HttpNotFound();
            }
            return View(equiepeFuncionarios);
        }

        // POST: EquiepeFuncionarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,intEquipeID_FK,intFuncID_FK,strTipo,intStatus")] EquiepeFuncionarios equiepeFuncionarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equiepeFuncionarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equiepeFuncionarios);
        }

        // GET: EquiepeFuncionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquiepeFuncionarios equiepeFuncionarios = db.EquiepeFuncionarios.Find(id);
            if (equiepeFuncionarios == null)
            {
                return HttpNotFound();
            }
            return View(equiepeFuncionarios);
        }

        // POST: EquiepeFuncionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquiepeFuncionarios equiepeFuncionarios = db.EquiepeFuncionarios.Find(id);
            db.EquiepeFuncionarios.Remove(equiepeFuncionarios);
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
