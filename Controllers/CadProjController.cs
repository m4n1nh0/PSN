using PSN2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSN2018.Controllers
{
    public class CadProjController : Controller
    {
        private PSN2018Context db = new PSN2018Context();

        // GET: CadProj
        public ActionResult Index()
        {
            objProjetoSec CricObj = new objProjetoSec();
            ViewBag.Clientes = db.Clientes.ToList();
            ViewBag.GP = db.GerenteProjetoes.ToList();
            CricObj.listAtividade = new List<AtivProj>();
            ViewBag.ListAtv = CricObj.listAtividade.ToList();
            CricObj.listAtivReq = new List<AtivRequi>();
            ViewBag.ListReq = CricObj.listAtivReq.ToList();

            return View();
        }





        private objProjetoSec GetObjProjetoSec()

        {

            if (Session["ops"] == null)

            {

                Session["ops"] = new objProjetoSec();

            }

            return (objProjetoSec)Session["ops"];

        }



        private void RemoveObjProjetoSec()

        {

            Session.Remove("ops");

        }




        [HttpPost]

        public ActionResult CadDetalhesProj(Projeto objProj, string BtnPrevious, string BtnNext)

        {

            if (BtnNext != null)

            {

                if (ModelState.IsValid)

                {

                    objProjetoSec CricObj = GetObjProjetoSec();

                    CricObj.strDsc = objProj.strDsc;

                    CricObj.strObjetivo = objProj.strObjetivo;

                    CricObj.intClienteID_FK = objProj.intClienteID_FK;

                    CricObj.intGPID_FK = objProj.intGPID_FK;

                    CricObj.dteCriacao = DateTime.Now;

                    CricObj.decPercProj = 0;

                    if (CricObj.listAtividade == null)
                    {
                        CricObj.listAtividade = new List<AtivProj>();
                    }
                    ViewBag.ListAtv = CricObj.listAtividade.ToList();

                    return View("Atividades");


                }

            }

            return View("Index");

        }


        [HttpPost]
        public ActionResult CadRequisitos(Requisito Req, string BtnPrevious, string BtnNext, string BtnAdd, string DscAtiv)

        {

            objProjetoSec CricObj = GetObjProjetoSec();
            

            if (BtnPrevious != null)

            {
              
                Atividade DetailsObj = new Atividade();

                DetailsObj.strDsc = CricObj.strDscAtividades;
                DetailsObj.dteDataIni = CricObj.dteDataIni;
                DetailsObj.dteDataFin = CricObj.dteDataFin;
                DetailsObj.intFuncID_FK = CricObj.intFuncID_FK;
                DetailsObj.intGrauDif = CricObj.intGrauDif;
                DetailsObj.intStatus = CricObj.intStatus;
                DetailsObj.intSprintID_FK = CricObj.intSprintID_FK;
                if (CricObj.listAtividade == null)
                {
                    CricObj.listAtividade = new List<AtivProj>();
                }
                ViewBag.ListAtv = CricObj.listAtividade.ToList();

                return View("Atividades", DetailsObj);

            }

            if (BtnNext != null)

            {

                if (ModelState.IsValid)

                {

                    CricObj.strDscReq = Req.strDsc;

                    Projeto auxProj = new Projeto();

                    auxProj.id = 0;
                    auxProj.strDsc = CricObj.strDsc;
                    auxProj.strObjetivo = CricObj.strDsc;
                    auxProj.intClienteID_FK = CricObj.intClienteID_FK;
                    auxProj.intGPID_FK = CricObj.intGPID_FK;
                    auxProj.dteCriacao = DateTime.Now;
                    auxProj.decPercProj = 0;
                    db.Projetoes.Add(auxProj);
                    db.SaveChanges();

                    Kanban kanban = new Kanban();
                    kanban.intProjetoID_FK = auxProj.id;
                    db.Kanbans.Add(kanban);
                    db.SaveChanges();

                    Sprint sprint = new Sprint();
                    sprint.intKanbanID_FK = kanban.id;
                    sprint.strDsc = "Backlog";
                    //sprint.dteDataIni = DateTime.Now;
                    //sprint.dteDataFin = DateTime.Now;
                    sprint.intSprintID = 0;
                    db.Sprints.Add(sprint);
                    db.SaveChanges();

                    if (CricObj.listAtividade.Count > 0)
                    {
                        foreach (AtivProj ativ in CricObj.listAtividade)
                        {
                            Atividade atividade = new Atividade();
                            atividade.strDsc = ativ.DscAtividades;
                            atividade.intGrauDif = ativ.GrauDif;
                            atividade.intSprintID_FK = sprint.id;
                            //atividade.dteDataIni = DateTime.Now;
                            //atividade.dteDataFin = DateTime.Now;
                            //atividade.dteDataPrev = DateTime.Now;
                            atividade.intFuncID_FK = 0;
                            atividade.intStatus = 0;
                            db.Atividades.Add(atividade);
                            db.SaveChanges();
                            int _id = atividade.id;
                            if (CricObj.listAtivReq.Count > 0)
                            {
                                foreach (AtivRequi ativreq in CricObj.listAtivReq)
                                {
                                    if (ativ.DscAtividades.Equals(ativreq.DscAtividades))
                                    {
                                        Requisito auxReq = new Requisito();
                                        auxReq.strDsc = ativreq.DescRequisito;
                                        db.Requisitoes.Add(auxReq);
                                        db.SaveChanges();
                                        int _idreq = auxReq.id;

                                        AtividadeRequerida ativrequi = new AtividadeRequerida();
                                        ativrequi.intAtividadeID_FK = _id;
                                        ativrequi.intRequisitoID_FK = _idreq;
                                        db.AtividadeRequeridas.Add(ativrequi);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }

                    sprint = new Sprint();
                    sprint.intKanbanID_FK = kanban.id;
                    sprint.strDsc = "A Fazer";
                    //sprint.dteDataIni = DateTime.Now;
                    //sprint.dteDataFin = DateTime.Now;
                    sprint.decPerc = 0;
                    sprint.intSprintID = 1;
                    db.Sprints.Add(sprint);
                    db.SaveChanges();

                    sprint = new Sprint();
                    sprint.intKanbanID_FK = kanban.id;
                    sprint.strDsc = "Desenvolvimento";
                    //sprint.dteDataIni = DateTime.Now;
                    //sprint.dteDataFin = DateTime.Now;
                    sprint.decPerc = 0;
                    sprint.intSprintID = 2;
                    db.Sprints.Add(sprint);
                    db.SaveChanges();

                    sprint = new Sprint();
                    sprint.intKanbanID_FK = kanban.id;
                    sprint.strDsc = "Teste";
                    //sprint.dteDataIni = DateTime.Now;
                    //sprint.dteDataFin = DateTime.Now;
                    sprint.decPerc = 0;
                    sprint.intSprintID = 3;
                    db.Sprints.Add(sprint);
                    db.SaveChanges();

                    sprint = new Sprint();
                    sprint.intKanbanID_FK = kanban.id;
                    sprint.strDsc = "Concluido";
                    //sprint.dteDataIni = DateTime.Now;
                    //sprint.dteDataFin = DateTime.Now;
                    sprint.decPerc = 0;
                    sprint.intSprintID = 4;
                    db.Sprints.Add(sprint);
                    db.SaveChanges();


                    RemoveObjProjetoSec();
                    return RedirectToAction("Index");
                }
            }

            if (BtnAdd != null)
            {
                AtivRequi ativRequi = new AtivRequi();
                ativRequi.DescRequisito = Req.strDsc;
                ativRequi.DscAtividades = DscAtiv;
                CricObj.listAtivReq.Add(ativRequi);
                ViewBag.ListReq = CricObj.listAtivReq.ToList();
                ViewBag.ListAtv = CricObj.listAtividade.ToList();
                return View("Requisitos");
            }

            return View("Requisitos");

        }

        
        [HttpPost]
        public ActionResult CadRAtividades(Atividade ativ, string BtnPrevious, string BtnNext, string BtnAdd)

        {

            objProjetoSec CricObj = GetObjProjetoSec();


            if (BtnPrevious != null)

            {

                Projeto DetailsObj = new Projeto();

                DetailsObj.strDsc = CricObj.strDsc;

                DetailsObj.strObjetivo = CricObj.strObjetivo;

                DetailsObj.intClienteID_FK = CricObj.intClienteID_FK;

                DetailsObj.intGPID_FK = CricObj.intGPID_FK;

                ViewBag.Clientes = db.Clientes.ToList();
                ViewBag.GP = db.GerenteProjetoes.ToList();

                return View("Index", DetailsObj);

            }

            if (BtnNext != null)

            {

                if (CricObj.listAtividade.Count() > 0)

                {

                    // Não precisa, quando chegar no ultimo crio as relações
                    //CricObj.strDscAtividades = ativ.strDsc;
                    //CricObj.dteDataIni = ativ.dteDataIni;
                    //CricObj.dteDataFin = ativ.dteDataFin;
                    //CricObj.intFuncID_FK = ativ.intFuncID_FK;
                    //CricObj.intGrauDif = ativ.intGrauDif;
                    //CricObj.intStatus = ativ.intStatus;
                    //CricObj.intSprintID_FK = ativ.intSprintID_FK;
                    if (CricObj.listAtivReq == null)
                    {
                        CricObj.listAtivReq = new List<AtivRequi>();
                    }
                    ViewBag.ListReq = CricObj.listAtivReq.ToList();
                    ViewBag.ListAtv = CricObj.listAtividade.ToList();
                    return View("Requisitos");

                }
                else
                {
                    CricObj.listAtividade = new List<AtivProj>();
                    ViewBag.ListAtv = CricObj.listAtividade.ToList();
                    ModelState.AddModelError("", "Nenhuma Atividade Adicionada!");
                    return View("Atividades");
                }

            }

            if (BtnAdd != null)
            {
                AtivProj ativProj = new AtivProj();
                ativProj.DscAtividades = ativ.strDsc;
                ativProj.GrauDif = ativ.intGrauDif;
                CricObj.listAtividade.Add(ativProj);
                ViewBag.ListAtv = CricObj.listAtividade.ToList();
                return View("Atividades");
            }

            return View("Atividades");

        }

        public ActionResult RemoverAtiv(string DscAtividades, int GrauDif)

        {
            objProjetoSec CricObj = GetObjProjetoSec();
            AtivProj ativProj = new AtivProj();
            ativProj.DscAtividades = DscAtividades;
            ativProj.GrauDif = GrauDif;
            var item = CricObj.listAtividade.First(x => x.DscAtividades == DscAtividades);
            CricObj.listAtividade.Remove(item);
            ViewBag.ListAtv = CricObj.listAtividade.ToList();
            return View("Atividades");
        }

        public ActionResult RemoverReq(string DscAtividades, string DescRequisito)

        {
            objProjetoSec CricObj = GetObjProjetoSec();
            AtivRequi ativRequi = new AtivRequi();
            ativRequi.DscAtividades = DscAtividades;
            ativRequi.DescRequisito = DescRequisito;
            var item = CricObj.listAtivReq.First(x => x.DescRequisito == DescRequisito);
            CricObj.listAtivReq.Remove(item);
            ViewBag.ListReq = CricObj.listAtivReq.ToList();
            ViewBag.ListAtv = CricObj.listAtividade.ToList();
            return View("Requisitos");
        }
    }
}