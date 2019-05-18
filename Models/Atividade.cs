using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Atividade
    {
        public int id { get; set; }


        [Display(Name = "Grau de Dificuldade")]
        [Required(ErrorMessage = "Informe o Grau de Dificuldade")]
        public int intGrauDif { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a Descrição da Atividade")]
        public string strDsc { get; set; }

        [Display(Name = "Inicio")]
        public Nullable<DateTime> dteDataIni { get; set; }
        [Display(Name = "Fim")]
        public Nullable<DateTime> dteDataFin { get; set; }
        [Display(Name = "Situação")]
        public int intStatus { get; set; }
        [Display(Name = "Sprint")]
        public int intSprintID_FK { get; set; }
        [Display(Name = "Funcionário")]
        public int intFuncID_FK { get; set; }
        [Display(Name = "Previsao")]
        public Nullable<DateTime> dteDataPrev { get; set; }
    }
}