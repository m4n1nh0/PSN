using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class GerenteProjeto
    {
        public int id { get; set; }

        [Display(Name = "CPF")]
        [StringLength(14)]
        [Required(ErrorMessage = "Informe o CPF")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF inválido.")]
        public string strCPF { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome permite no máximo 100 caracteres!")]
        public string strNome { get; set; }
    }
}