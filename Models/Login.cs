using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Login
    {
        public int id { get; set; }

        [Display(Name = "CPF/CNPJ:")]
        [StringLength(20)]
        [Required(ErrorMessage = "Informe o CPF/CNPJ")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF/CNPJ inválido.")]
        public string strCPFCNPJ { get; set; }

        [Display(Name = "Senha:")]
        [StringLength(50, MinimumLength = 6)]
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string strSenha { get; set; }

        [Display(Name = "Nome:")]
        public string strNome { get; set; }

        [Display(Name = "Nível:")]
        public int intNivel { get; set; }

        [Display(Name = "Imagem Perfil:")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

    }
}