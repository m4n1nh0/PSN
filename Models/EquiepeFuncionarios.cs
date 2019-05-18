using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class EquiepeFuncionarios
    {
        public int id { get; set; }

        [Display(Name = "Código Equipe:")]
        public int intEquipeID_FK { get; set; }

        [Display(Name = "Código do Funcionário:")]
        public int intFuncID_FK { get; set; }

        [Display(Name = "Tipo Função:")]
        [StringLength(15, ErrorMessage = "O campo Tipo permite no máximo 15 caracteres!")]
        [Required(ErrorMessage = "Informe o Tipo")]
        public string strTipo { get; set; }

        [Display(Name = "Status:")]
        public Nullable<int> intStatus { get; set; }
    }

    public class listEquipeFunc
    {
        public List<FunciEquipe> listEquipe { get; set; }
    }

    public class FunciEquipe
    {
        //public int id { get; set; }
        public int intEquipeID_FK { get; set; }
        public string strNomeEquipe { get; set; }
        public int intFuncID_FK { get; set; }
        public string strNomeFunc { get; set; }
        public string strTipo { get; set; }
        public Nullable<int> intStatus { get; set; }
    }
}