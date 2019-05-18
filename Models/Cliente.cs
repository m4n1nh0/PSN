using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Cliente
    {
        public int id { get; set; }

        [Display(Name = "CPF/CNPJ:")]
        [StringLength(20)]
        [Required(ErrorMessage = "Informe o CPF/CNPJ")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF/CNPJ inválido.")]
        public string strCPFCNPJ { get; set; }

        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "Informe o Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome permite no máximo 100 caracteres!")]
        public string strNome { get; set; }

    }
}