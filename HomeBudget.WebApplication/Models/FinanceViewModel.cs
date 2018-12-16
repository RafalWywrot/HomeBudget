using HomeBudget.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Models
{
    public class FinanceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}