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
        /// <summary>
        /// Add finance
        /// </summary>
        /// <param name="entity">Finance entity</param>
        public void Add(Finance entity)
        {
            entity.CreateDateTime = DateTime.Now;
            _context.Finances.Add(entity);
        }
        /// <summary>
        /// Delete finance
        /// </summary>
        /// <param name="entity">Finance entity</param>
        public void Delete(Finance entity)
        {
            _context.Finances.Remove(entity);
        }
        /// <summary>
        /// Get first founded finance with specific query
        /// </summary>
        /// <param name="predicate">Func limited to specific record</param>
        /// <returns>Finance entity</returns>
        public Finance GetDetail(Func<Finance, bool> predicate)
        {
            return _context.Finances.Where(predicate).FirstOrDefault();
        }
        /// <summary>
        /// Get all founded finances with specific query
        /// </summary>
        /// <param name="predicate">Func limited to specific records</param>
        /// <returns>Finance entities</returns>
        public IEnumerable<Finance> GetOverview(Func<Finance, bool> predicate = null)
        {
            if (predicate != null)
                return _context.Finances.Where(predicate);
            return _context.Finances;
        }
    }
}