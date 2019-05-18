using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class AtividadeRequerida
    {
        public int id { get; set; }

        [Display(Name = "Código Atividade:")]
        public int intAtividadeID_FK { get; set; }

        [Display(Name = "Código Requisito:")]
        public int intRequisitoID_FK { get; set; }
    }
}