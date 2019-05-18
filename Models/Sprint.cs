using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Sprint
    {
        public int id { get; set; }

        [Display(Name = "Código do Kanban:")]
        public int intKanbanID_FK { get; set; }

        [Display(Name = "Código da Sprint:")]
        public int intSprintID { get; set; }

        [Display(Name = "Descrição:")]
        [StringLength(50, ErrorMessage = "O campo Descrição permite no máximo 50 caracteres!")]
        [Required(ErrorMessage = "Informe a Descrição")]
        public string strDsc { get; set; }

        [Display(Name = "Data Início:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public Nullable<System.DateTime> dteDataIni { get; set; }

        [Display(Name = "Data de Entrega:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public Nullable<System.DateTime> dteDataFin { get; set; }

        [Display(Name = "Percentual:")]
        public Nullable<decimal> decPerc { get; set; }
    }
}