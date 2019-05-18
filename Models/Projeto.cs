using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Projeto
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
    }
}