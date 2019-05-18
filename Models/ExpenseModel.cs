using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSN2018.Models
{
    public class ExpenseModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Expense Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Amount")]
        public string Amount { get; set; }
    }
}