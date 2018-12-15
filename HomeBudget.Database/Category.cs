using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Database
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExpense { get; set; }
    }
}
