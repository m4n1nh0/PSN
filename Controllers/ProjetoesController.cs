using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSN2018.Models;
using System.ComponentModel.DataAnnotations;

namespace PSN2018.Controllers
{
    public class ProjetoesController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: Projetoes
        public ActionResult Index()
        {
            //ViewBag.ListaDocs = db.UploadFileResults.ToList();
            List<ListProjeto> listproj = new List<ListProjeto>();
            var projeto = db.Projetoes.ToList();
            foreach (var P in projeto)
            {
                ListProjeto listProjeto = new ListProjeto();
                listProjeto.Equipe = new List<Equipes>();
                var consulta = db.Equipes.Where(a => a.intProjetoID_FK.Equals(P.id)).FirstOrDefault(); ;
                listProjeto.strNomeEquipe = "Não Cadastrada";
                if (consulta != null)
                {
                    listProjeto.strNomeEquipe = consulta.strDsc;
                    var Eqpfunc = db.EquiepeFuncionarios.Where(e => e.intEquipeID_FK.Equals(consulta.id)).ToList();
                    if (Eqpfunc != null)
                    {
                        foreach (var e in Eqpfunc)
                        {
                            Funcionario funci = db.Funcionarios.Find(e.intFuncID_FK);
                            Equipes equipes = new Equipes();
                            equipes.imgCPF = funci.strCPF;
                            listProjeto.Equipe.Add(equipes);
                        }
                    }
                }

                listProjeto.id = P.id;
                listProjeto.dteCriacao = P.dteCriacao;
                listProjeto.decPercProj = P.decPercProj;
                listProjeto.intClienteID_FK = P.intClienteID_FK;
                listProjeto.intGPID_FK = P.intGPID_FK;
                listProjeto.strDsc = P.strDsc;
                listProjeto.strObjetivo = P.strObjetivo;
                listproj.Add(listProjeto);
            }

            ViewBag.Projetos = listproj;
            return View();
        }

        // GET: Projetoes/Details/5
        public ActionResult Details(int? id)
        {

            var kanbam = db.Kanbans.Where(kb => kb.intProjetoID_FK == id).FirstOrDefault();
            ViewBag.kanbam = db.Kanbans.Where(kb => kb.intProjetoID_FK == id).ToList();
            ViewBag.Sprint = db.Sprints.Where(s => s.intKanbanID_FK == kanbam.id).ToList();
            ViewBag.Ativi = db.Atividades.ToList();
            ViewBag.ListaDocs = db.UploadFileResults.Where(up => up.intProjetoID_FK == id).ToList();
            ViewBag.NomeCliente = nomCliente(id);
            ViewBag.NomeGerente = nomGp(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetoes.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        [HttpPost]
        public ActionResult Details(int? id, string BtnProx, string BtnPrev, FormCollection form)
        {
            var kanbam = db.Kanbans.Where(kb => kb.intProjetoID_FK == id).FirstOrDefault();
            if (BtnProx != null)
            {
                string[] AllStrings = form["ativs"].Split(',');
                foreach (string item in AllStrings)
                {
                    int value = int.Parse(item);
                    var itemList = db.Atividades.Find(value);
                    var sprint = db.Sprints.Where(s => s.intKanbanID_FK == kanbam.id).ToList();
                    int chave = 0;
                    int next = 0;
                    foreach (Sprint spt in sprint)
                    {
                        if (spt.id == itemList.intSprintID_FK)
                        {
                            itemList.intStatus = 1;
                            db.Entry(itemList).State = EntityState.Modified;
                            db.SaveChanges();
                            next = spt.intSprintID + 1;
                        }
                        else
                        {
                            if (spt.id > itemList.intSprintID_FK && spt.intSprintID == next)
                            {
                                chave = spt.id;
                            }
                        }
                    }
                    if (chave > 0)
                    {
                        Atividade atividade = new Atividade();
                        atividade.intGrauDif = itemList.intGrauDif;
                        atividade.strDsc = itemList.strDsc;
                        atividade.intSprintID_FK = chave;
                        atividade.intStatus = 0;
                        atividade.dteDataIni = DateTime.Now;
                        db.Atividades.Add(atividade);
                        db.SaveChanges();
                    }
                }
            }
            if (BtnPrev != null)
            {
                string[] AllStrings = form["ativs"].Split(',');
                foreach (string item in AllStrings)
                {
                    int value = int.Parse(item);
                    var itemList = db.Atividades.Find(value);
                    var sprint = db.Sprints.Where(s => s.intKanbanID_FK == kanbam.id).ToList();
                    int chave = 0;
                    int prev = 0;
                    foreach (Sprint spt in sprint)
                    {
                        if (spt.id == itemList.intSprintID_FK)
                        {
                            itemList.intStatus = 1;
                            db.Atividades.Remove(itemList);
                            db.SaveChanges();
                            prev = spt.intSprintID - 1;
                        }
                    }

                    foreach (Sprint spt in sprint)
                    {
                        if (spt.id < itemList.intSprintID_FK && spt.intSprintID == prev )
                        {
                            chave = spt.id;
                        }
                    }
                    Atividade atividade = db.Atividades.Where(a => a.strDsc == itemList.strDsc).First();
                    if (atividade != null)
                    {
                        atividade.intStatus = 0;
                        atividade.dteDataFin = null;
                        db.Entry(atividade).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
            }
            ViewBag.kanbam = db.Kanbans.Where(kb => kb.intProjetoID_FK == id).ToList();
            ViewBag.Sprint = db.Sprints.Where(s => s.intKanbanID_FK == kanbam.id).ToList();
            ViewBag.Ativi = db.Atividades.ToList();
            ViewBag.ListaDocs = db.UploadFileResults.Where(up => up.intProjetoID_FK == id).ToList();
            ViewBag.NomeCliente = nomCliente(id);
            ViewBag.NomeGerente = nomGp(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetoes.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // GET: Projetoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projetoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,strDsc,strObjetivo,intClienteID_FK,intGPID_FK,dteCriacao,decPercProj")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                projeto.dteCriacao = DateTime.Now;
                db.Projetoes.Add(projeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projeto);
        }

        // GET: Projetoes/Edit/5
        public ActionResult Edit(int? id)
        {
            Session["id"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetoes.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: Projetoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,strDsc,strObjetivo,intClienteID_FK,intGPID_FK,dteCriacao,decPercProj")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projeto);
        }

        // GET: Projetoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetoes.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: Projetoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projeto projeto = db.Projetoes.Find(id);
            db.Projetoes.Remove(projeto);
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

        private String nomCliente(int?id)
        {
            String nome = null;
            var projeto = db.Projetoes.Find(id);
            if (projeto != null)
            {
                var cliente = db.Clientes.Find(projeto.intClienteID_FK);
                if (cliente != null)
                {
                    nome = cliente.strNome;
                }
            }
            return nome;
        }

        private String nomGp(int? id)
        {
            String nomeGp = null;
            var projeto = db.Projetoes.Find(id);
            if (projeto != null)
            {
                var gp = db.GerenteProjetoes.Find(projeto.intGPID_FK);
                if (gp != null)
                {
                    nomeGp = gp.strNome;
                }
            }
            return nomeGp;
        }
    }
    public class ListProjeto
    {
        public int id { get; set; }

        [Display(Name = "Descrição:")]
        [StringLength(100, ErrorMessage = "O campo Descrição permite no máximo 100 caracteres!")]
        [Required(ErrorMessage = "Informe a Descrição")]
        public string strDsc { get; set; }

        [Display(Name = "Objetivo:")]
        [StringLength(500, ErrorMessage = "O campo Objetivo permite no máximo 500 caracteres!")]
        [Required(ErrorMessage = "Informe o Objetivo")]
        public string strObjetivo { get; set; }

        [Display(Name = "Código do Cliente:")]
        public int intClienteID_FK { get; set; }

        [Display(Name = "Código do GP:")]
        public int intGPID_FK { get; set; }

        [Display(Name = "data Cadastro:")]
        public DateTime dteCriacao { get; set; }

        [Display(Name = "Perc Projeto:")]
        public Decimal decPercProj { get; set; }

        public string strNomeEquipe { get; set; }
        public List<Equipes> Equipe { get; set; }

    }

    public class Equipes
    {
        public string imgCPF { get; set; }
    }

   
}
