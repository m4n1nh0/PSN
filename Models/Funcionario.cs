using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class Funcionario
    {

        
        public int id { get; set; }

        [Display(Name = "CPF:")]
        [StringLength(14)]
        [Required(ErrorMessage = "Informe o CPF")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF inválido.")]
        public string strCPF { get; set; }

        [Display(Name = "Nome:")]
        [StringLength(100, ErrorMessage = "O campo Nome permite no máximo 100 caracteres!")]
        [Required(ErrorMessage = "Informe o Nome")]
        public string strNome { get; set; }

        [Display(Name = "Produtividade:")]
        public double dblProdutiv { get; set; }

        [Display(Name = "Quantidade de Atavidade:")]
        public int intAtividades { get; set; }

        [Display(Name = "Codigo Funcao:")]
        public int intFuncaoID_FK { get; set; }

        #region Objetos fora do Banco
        public string strFuncao { get; set; }
        #endregion
    }

    public class FuncSkill
    {
        public int id;

        [Display(Name = "Nome da Skill:")]
        [StringLength(50, ErrorMessage = "O campo Descrição permite no máximo 50 caracteres!")]
        public string strskill { get; set; }

        [Display(Name = "Nível:")]
        public decimal decPerc { get; set; }

    }

    public class listFuncSkill
    {
        public List<FuncSkill> listSkill { get; set; }
    }
}

