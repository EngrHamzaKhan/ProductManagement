using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductManagement.Web.Data;
using ProductManagement.Web.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Web.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IDbContextTransaction _transaction;

        // Constructor to accept a transaction
        public EfRepository(ApplicationDbContext context, IDbContextTransaction transaction)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _transaction = transaction;
        }

        // Get all items from the database
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Get a single item by ID
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Insert a new item
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Update an existing item
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        // Delete an item by ID
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

    }
}
