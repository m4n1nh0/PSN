using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Relatorio
    {
        public int id { get; set; }

        [Display(Name = "Código da Reunião:")]
        public int intReuniaoID_FK { get; set; }

        [Display(Name = "Código do Relátorio:")]
        public int intRelatorioID { get; set; }

        [Display(Name = "Descrição:")]
        [StringLength(1000, ErrorMessage = "O campo Descrição permite no máximo 1000 caracteres!")]
        [Required(ErrorMessage = "Informe a Descrição")]
        public string strDsc { get; set; }
    }
}