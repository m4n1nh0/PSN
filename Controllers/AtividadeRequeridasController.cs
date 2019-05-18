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
    public class AtividadeRequeridasController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: AtividadeRequeridas
        public ActionResult Index()
        {
            return View(db.AtividadeRequeridas.ToList());
        }

        // GET: AtividadeRequeridas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtividadeRequerida atividadeRequerida = db.AtividadeRequeridas.Find(id);
            if (atividadeRequerida == null)
            {
                return HttpNotFound();
            }
            return View(atividadeRequerida);
        }

        // GET: AtividadeRequeridas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AtividadeRequeridas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,intAtividadeID_FK,intRequisitoID_FK")] AtividadeRequerida atividadeRequerida)
        {
            if (ModelState.IsValid)
            {
                db.AtividadeRequeridas.Add(atividadeRequerida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(atividadeRequerida);
        }

        // GET: AtividadeRequeridas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtividadeRequerida atividadeRequerida = db.AtividadeRequeridas.Find(id);
            if (atividadeRequerida == null)
            {
                return HttpNotFound();
            }
            return View(atividadeRequerida);
        }

        // POST: AtividadeRequeridas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,intAtividadeID_FK,intRequisitoID_FK")] AtividadeRequerida atividadeRequerida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atividadeRequerida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atividadeRequerida);
        }

        // GET: AtividadeRequeridas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtividadeRequerida atividadeRequerida = db.AtividadeRequeridas.Find(id);
            if (atividadeRequerida == null)
            {
                return HttpNotFound();
            }
            return View(atividadeRequerida);
        }

        // POST: AtividadeRequeridas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AtividadeRequerida atividadeRequerida = db.AtividadeRequeridas.Find(id);
            db.AtividadeRequeridas.Remove(atividadeRequerida);
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
