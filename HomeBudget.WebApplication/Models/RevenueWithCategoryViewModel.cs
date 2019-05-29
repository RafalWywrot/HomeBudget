using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class RevenueWithCategoryViewModel
    {
        public List<RevenueViewModel> Revenues { get; set; }
        public List<HomeBudget.Database.Category> Categories { get; set; }
    }
}