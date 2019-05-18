using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSN2018.Models;
using System.IO;

namespace PSN2018.Controllers
{
    public class UploadFileResultsController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: UploadFileResults
        public ActionResult Index()
        {
            return View(db.UploadFileResults.ToList());
        }

        // GET: UploadFileResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFileResult uploadFileResult = db.UploadFileResults.Find(id);
            if (uploadFileResult == null)
            {
                return HttpNotFound();
            }
            return View(uploadFileResult);
        }

        // GET: UploadFileResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadFileResults/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nome,Tamanho,Tipo,Caminho,intProjetoID_FK")] UploadFileResult uploadFileResult)
        {
            if (ModelState.IsValid)
            {
                db.UploadFileResults.Add(uploadFileResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uploadFileResult);
        }

        // GET: UploadFileResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFileResult uploadFileResult = db.UploadFileResults.Find(id);
            if (uploadFileResult == null)
            {
                return HttpNotFound();
            }
            return View(uploadFileResult);
        }

        // POST: UploadFileResults/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nome,Tamanho,Tipo,Caminho,intProjetoID_FK")] UploadFileResult uploadFileResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploadFileResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uploadFileResult);
        }

        // GET: UploadFileResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFileResult uploadFileResult = db.UploadFileResults.Find(id);
            if (uploadFileResult == null)
            {
                return HttpNotFound();
            }
            return View(uploadFileResult);
        }

        // POST: UploadFileResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadFileResult uploadFileResult = db.UploadFileResults.Find(id);
            db.UploadFileResults.Remove(uploadFileResult);
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

        [HttpPost, ActionName("FileUpload")]
        public ActionResult FileUpload(FormCollection fc, HttpPostedFileBase file)
        {
            int idProjSession = Int32.Parse(Session["id"].ToString());
            UploadFileResult tbl = new UploadFileResult();
            var listaExtensions = new[] {".doc", ".png", ".jpg", ".pdf", ".xml" };
            tbl.intProjetoID_FK = idProjSession;
            tbl.Caminho = file.ToString(); 
            tbl.Nome = fc["Name"].ToString();
            var fileName = Path.GetFileName(file.FileName);   
            var ext = Path.GetExtension(file.FileName); 

            tbl.Tipo = ext;
            tbl.Tamanho = 0;
            if (listaExtensions.Contains(ext)) 
            {
                string name = Path.GetFileNameWithoutExtension(fileName);  
                string myfile = name + "_" + tbl.intProjetoID_FK + ext; 
                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), myfile);
                tbl.Caminho = path;
                db.UploadFileResults.Add(tbl);
                db.SaveChanges();
                file.SaveAs(path);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Tipo de arquivo não permitido.";
            }
            return View();
        }


    }
}
