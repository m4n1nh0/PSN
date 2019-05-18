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
    public class SkillFuncionariosController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: SkillFuncionarios
        public ActionResult Index()
        {
            return View(db.SkillFuncionarios.ToList());
        }

        // GET: SkillFuncionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillFuncionarios skillFuncionarios = db.SkillFuncionarios.Find(id);
            if (skillFuncionarios == null)
            {
                return HttpNotFound();
            }
            return View(skillFuncionarios);
        }

        // GET: SkillFuncionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillFuncionarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,intFuncID_FK,intSkillID_FK,decPerc")] SkillFuncionarios skillFuncionarios)
        {
            if (ModelState.IsValid)
            {
                db.SkillFuncionarios.Add(skillFuncionarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillFuncionarios);
        }

        // GET: SkillFuncionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillFuncionarios skillFuncionarios = db.SkillFuncionarios.Find(id);
            if (skillFuncionarios == null)
            {
                return HttpNotFound();
            }
            return View(skillFuncionarios);
        }

        // POST: SkillFuncionarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,intFuncID_FK,intSkillID_FK,decPerc")] SkillFuncionarios skillFuncionarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillFuncionarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skillFuncionarios);
        }

        // GET: SkillFuncionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillFuncionarios skillFuncionarios = db.SkillFuncionarios.Find(id);
            if (skillFuncionarios == null)
            {
                return HttpNotFound();
            }
            return View(skillFuncionarios);
        }

        // POST: SkillFuncionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillFuncionarios skillFuncionarios = db.SkillFuncionarios.Find(id);
            db.SkillFuncionarios.Remove(skillFuncionarios);
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
