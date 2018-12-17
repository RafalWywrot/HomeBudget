using HomeBudget.WebApplication.Helpers.ValidatinAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class RevenueViewModel : FinanceViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [MinValue(0.01, ErrorMessage = "Wartość w polu {0} musi być większa lub równa 0.01")]
        [MaxValue(double.MaxValue, ErrorMessage = "Wartość w polu {0} jest za duża")]
        [Display(Name = "Cena")]
        public double Price { get; set; }
    }
}