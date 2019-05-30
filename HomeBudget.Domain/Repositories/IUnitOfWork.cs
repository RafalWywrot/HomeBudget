using HomeBudget.Database;
using HomeBudget.Domain.Interfaces;
using System;

namespace HomeBudget.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Category> CategoryRepository { get; }
        IRepository<Finance> FinanceRepository { get; }
        void SaveChanges();
    }
}
