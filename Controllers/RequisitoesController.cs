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
    public class RequisitoesController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: Requisitoes
        public ActionResult Index()
        {
            return View(db.Requisitoes.ToList());
        }

        // GET: Requisitoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisito requisito = db.Requisitoes.Find(id);
            if (requisito == null)
            {
                return HttpNotFound();
            }
            return View(requisito);
        }

        // GET: Requisitoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requisitoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,strDsc")] Requisito requisito)
        {
            if (ModelState.IsValid)
            {
                db.Requisitoes.Add(requisito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requisito);
        }

        // GET: Requisitoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisito requisito = db.Requisitoes.Find(id);
            if (requisito == null)
            {
                return HttpNotFound();
            }
            return View(requisito);
        }

        // POST: Requisitoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,strDsc")] Requisito requisito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requisito);
        }

        // GET: Requisitoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisito requisito = db.Requisitoes.Find(id);
            if (requisito == null)
            {
                return HttpNotFound();
            }
            return View(requisito);
        }

        // POST: Requisitoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisito requisito = db.Requisitoes.Find(id);
            db.Requisitoes.Remove(requisito);
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
