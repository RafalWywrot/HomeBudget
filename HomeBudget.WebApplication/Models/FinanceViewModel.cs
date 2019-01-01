using HomeBudget.Database;
using HomeBudget.WebApplication.Helpers.ValidatinAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class FinanceViewModel
    {
        public FinanceViewModel()
        {
            TimeEvent = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Data utworzenia wpisu")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateDateTimeDatabase { get; set; }
        [Required]
        [Display(Name = "Data przychodu")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawna data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateBirthRangeAttribute]
        public DateTime TimeEvent { get; set; }
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}