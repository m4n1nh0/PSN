using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Reuniao
    {
        public int id { get; set; }

        [Display(Name = "Data:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime dteData { get; set; }

        [Display(Name = "CPF/CNPJ:")]
        public string strCPFCNPJ { get; set; }

        [Display(Name = "Titulo:")]
        [StringLength(100, ErrorMessage = "O campo Titulo permite no máximo 100 caracteres!")]
        [Required(ErrorMessage = "Informe o Titulo")]
        public string strTitulo { get; set; }

        [Display(Name = "Descrição:")]
        [StringLength(200, ErrorMessage = "O campo Descrição permite no máximo 200 caracteres!")]
        [Required(ErrorMessage = "Informe a Descrição")]
        public string strDsc { get; set; }

        [Display(Name = "Status:")]
        public Nullable<int> intStatus { get; set; }

        [Display(Name = "Código do Projeto:")]
        public int intProjetoID_FK { get; set; }
    }
}