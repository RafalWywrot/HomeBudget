using HomeBudget.Database;
using HomeBudget.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Domain.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public CategoryRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Add category to context
        /// </summary>
        /// <param name="entity">Category entity</param>
        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get first founded category with specific query
        /// </summary>
        /// <param name="predicate">Func limited to specific record</param>
        /// <returns>Category entity</returns>
        public Category GetDetail(Func<Category, bool> predicate)
        {
            return _context.Categories.FirstOrDefault(predicate);
        }
        /// <summary>
        /// Get all founded categories with specific query
        /// </summary>
        /// <param name="predicate">Func limited to specific records</param>
        /// <returns>Category entities</returns>
        public IEnumerable<Category> GetOverview(Func<Category, bool> predicate = null)
        {
            if (predicate != null)
                return _context.Categories.Where(predicate);
            return _context.Categories;
        }
    }
}
