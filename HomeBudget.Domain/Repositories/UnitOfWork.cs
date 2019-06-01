using HomeBudget.Database;
using HomeBudget.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context = null;
        private IRepository<Category> _categoryRepository = null;
        private IRepository<Finance> _financeRepository = null;
        public UnitOfWork()
        {
            this._context = new ApplicationDbContext();
        }
        /// <summary>
        /// Context to category table
        /// </summary>
        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }
        /// <summary>
        /// Context to finance table
        /// </summary>
        public IRepository<Finance> FinanceRepository
        {
            get
            {
                if (_financeRepository == null)
                {
                    _financeRepository = new FinanceRepository(_context);
                }
                return _financeRepository;
            }
        }
        /// <summary>
        /// Save all records added to context
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
