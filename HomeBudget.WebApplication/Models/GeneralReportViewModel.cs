using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class GeneralReportViewModel
    {
        //[DataType(DataType.Date, ErrorMessage = "InvalidDatetime")]
        [Display(Name = "Data")]
        [Required(ErrorMessage = "Pole wymagane")] 
        //[DisplayFormat(DataFormatString = "{0:YYYY-MM-DD}", ApplyFormatInEditMode = true)]
        public DateTime DateReport { get; set; }
        public double Revenues { get; set; }
        public double Expenses { get; set; }
        public bool AnyCashOperation { get; set; }
    }
}