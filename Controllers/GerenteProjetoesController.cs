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
    public class GerenteProjetoesController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: GerenteProjetoes
        public ActionResult Index()
        {
            return View(db.GerenteProjetoes.ToList());
        }

        // GET: GerenteProjetoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GerenteProjeto gerenteProjeto = db.GerenteProjetoes.Find(id);
            if (gerenteProjeto == null)
            {
                return HttpNotFound();
            }
            return View(gerenteProjeto);
        }

        // GET: GerenteProjetoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GerenteProjetoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,strCPF,strNome")] GerenteProjeto gerenteProjeto)
        {
            if (ModelState.IsValid)
            {
                db.GerenteProjetoes.Add(gerenteProjeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gerenteProjeto);
        }

        // GET: GerenteProjetoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GerenteProjeto gerenteProjeto = db.GerenteProjetoes.Find(id);
            if (gerenteProjeto == null)
            {
                return HttpNotFound();
            }
            return View(gerenteProjeto);
        }

        // POST: GerenteProjetoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,strCPF,strNome")] GerenteProjeto gerenteProjeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gerenteProjeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gerenteProjeto);
        }

        // GET: GerenteProjetoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GerenteProjeto gerenteProjeto = db.GerenteProjetoes.Find(id);
            if (gerenteProjeto == null)
            {
                return HttpNotFound();
            }
            return View(gerenteProjeto);
        }

        // POST: GerenteProjetoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GerenteProjeto gerenteProjeto = db.GerenteProjetoes.Find(id);
            db.GerenteProjetoes.Remove(gerenteProjeto);
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
