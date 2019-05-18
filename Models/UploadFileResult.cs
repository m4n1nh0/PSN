using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class UploadFileResult
    {

        public int id { get; set; }
        [Display(Name = "Nome:")]
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public string Tipo { get; set; }

        [Display(Name = "Documento Projeto:")]
        public string Caminho { get; set; }

        [Display(Name = "Projeto:")]
        public int intProjetoID_FK { get; set; }
    }
}