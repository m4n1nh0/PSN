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
    public class KanbansController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: Kanbans
        public ActionResult Index()
        {
            return View(db.Kanbans.ToList());
        }

        // GET: Kanbans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kanban kanban = db.Kanbans.Find(id);
            if (kanban == null)
            {
                return HttpNotFound();
            }
            return View(kanban);
        }

        // GET: Kanbans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kanbans/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,intProjetoID_FK")] Kanban kanban)
        {
            if (ModelState.IsValid)
            {
                db.Kanbans.Add(kanban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kanban);
        }

        // GET: Kanbans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kanban kanban = db.Kanbans.Find(id);
            if (kanban == null)
            {
                return HttpNotFound();
            }
            return View(kanban);
        }

        // POST: Kanbans/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,intProjetoID_FK")] Kanban kanban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kanban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kanban);
        }

        // GET: Kanbans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kanban kanban = db.Kanbans.Find(id);
            if (kanban == null)
            {
                return HttpNotFound();
            }
            return View(kanban);
        }

        // POST: Kanbans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kanban kanban = db.Kanbans.Find(id);
            db.Kanbans.Remove(kanban);
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
