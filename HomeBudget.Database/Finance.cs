using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Database
{
    public class Finance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDateTime{ get; set; }
        public DateTime TimeEvent { get; set; } 
        public double Value { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
