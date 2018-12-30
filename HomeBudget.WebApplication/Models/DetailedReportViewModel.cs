using HomeBudget.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class DetailedReportViewModel
    {
        public List<RevenueViewModel> Revenues { get; set; }
        public List<RevenueViewModel> Expenses { get; set; }
        public DateTime DateReport { get; set; }
        public bool AnyCashOperation { get; set; }
        public List<Category> Categories { get; set; }
    }
}