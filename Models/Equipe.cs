using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Equipe
    {
        public int id { get; set; }

        [Display(Name = "Nome da Equipe:")]
        [StringLength(50, ErrorMessage = "O campo Nome da Equipe permite no máximo 50 caracteres!")]
        [Required(ErrorMessage = "Informe o Nome da Equipe")]
        public string strDsc { get; set; }

        [Display(Name = "Código do Projeto:")]
        public int intProjetoID_FK { get; set; }
    }
}