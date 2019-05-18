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
    public class FuncaosController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: Funcaos
        public ActionResult Index()
        {
            return View(db.Funcaos.ToList());
        }

        // GET: Funcaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcao funcao = db.Funcaos.Find(id);
            if (funcao == null)
            {
                return HttpNotFound();
            }
            return View(funcao);
        }

        // GET: Funcaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcaos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,strDsc")] Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                db.Funcaos.Add(funcao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funcao);
        }

        // GET: Funcaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcao funcao = db.Funcaos.Find(id);
            if (funcao == null)
            {
                return HttpNotFound();
            }
            return View(funcao);
        }

        // POST: Funcaos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,strDsc")] Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcao);
        }

        // GET: Funcaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcao funcao = db.Funcaos.Find(id);
            if (funcao == null)
            {
                return HttpNotFound();
            }
            return View(funcao);
        }

        // POST: Funcaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcao funcao = db.Funcaos.Find(id);
            db.Funcaos.Remove(funcao);
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
