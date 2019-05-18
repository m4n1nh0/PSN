using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class SkillFuncionarios
    {
        public int id { get; set; }

        [Display(Name = "Código Funcionário:")]
        public int intFuncID_FK { get; set; }

        [Display(Name = "Código da Skill:")]
        public int intSkillID_FK { get; set; }

        [Display(Name = "Nível:")]
        //[StringLength(4, ErrorMessage = "O campo Descrição permite no máximo 4 caracteres!")]
        //[Required(ErrorMessage = "Informe a Descrição")]
        public decimal decPerc { get; set; }
    }
}