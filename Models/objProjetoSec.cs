using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class objProjetoSec
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string strDsc { get; set; }

        [Required]
        public string strObjetivo { get; set; }

        [Required]
        public int intClienteID_FK { get; set; }

        [Required]
        public int intGPID_FK { get; set; }

        [Required]
        public string strDscReq { get; set; }

        [Required]
        public int intGrauDif { get; set; }

        [Required]
        public string strDscAtividades { get; set; }

        public DateTime dteDataIni { get; set; }
       
        public DateTime dteDataFin { get; set; }
       
        public int intStatus { get; set; }
    
        public int intSprintID_FK { get; set; }
     
        public int intFuncID_FK { get; set; }

        public List<AtivProj> listAtividade { get; set; }
        public List<AtivRequi> listAtivReq { get; set; }

        public DateTime dteCriacao { get; set; }
        public Decimal decPercProj { get; set; }

    }

    public class AtivProj
    {
        public int GrauDif { get; set; }

        public string DscAtividades { get; set; }

    }

    public class AtivRequi
    {
        public string DscAtividades { get; set; }

        public string DescRequisito { get; set; }

    }

  
   
}