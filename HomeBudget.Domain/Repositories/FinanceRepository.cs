using HomeBudget.Database;
using HomeBudget.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Domain.Repositories
{
    public class FinanceRepository : IRepository<Finance>
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public FinanceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Add(Finance entity)
        {
            entity.CreateDateTime = DateTime.Now;
            _context.Finances.Add(entity);
        }

        public void Delete(Finance entity)
        {
            _context.Finances.Remove(entity);
        }

        public Finance GetDetail(Func<Finance, bool> predicate)
        {
            return _context.Finances.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<Finance> GetOverview(Func<Finance, bool> predicate = null)
        {
            if (predicate != null)
                return _context.Finances.Where(predicate);
            return _context.Finances;
        }
    }
}