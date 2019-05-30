using HomeBudget.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.WebApplication.Tests
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Domain.Interfaces.IRepository<HomeBudget.Database.Category> CategoryRepository => throw new NotImplementedException();

        public Domain.Interfaces.IRepository<HomeBudget.Database.Finance> FinanceRepository => throw new NotImplementedException();

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
