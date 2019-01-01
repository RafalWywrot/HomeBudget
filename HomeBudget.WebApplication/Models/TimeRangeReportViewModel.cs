using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class TimeRangeReportViewModel : DetailedReportViewModel
    {
        [Display(Name = "Od")]
        [Required(ErrorMessage = "Pole wymagane")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Do")]
        [Required(ErrorMessage = "Pole wymagane")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
    }
}