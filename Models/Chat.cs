using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Chat
    {
        public int id { get; set; }

        [Display(Name = "Data:")]
        public DateTime dteData { get; set; }

        [Display(Name = "CPF/CNPJ:")]
        public string strCPFCNPJ_Sender { get; set; }

        [Display(Name = "CPF/CNPJ:")]
        public string strCPFCNPJ_Receiver { get; set; }

        [Display(Name = "Mensagem:")]
        public string strMensagem { get; set; }
    }
}