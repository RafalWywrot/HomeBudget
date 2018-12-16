using HomeBudget.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Domain.Interfaces
{
    public interface IReveneusService
    {
        List<Category> GetCategories();
    }
}
