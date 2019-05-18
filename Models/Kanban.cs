using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Kanban
    {
        public int id { get; set; }

        [Display(Name = "Código do Quadro Kanban:")]
        public int intProjetoID_FK { get; set; }
    }
}