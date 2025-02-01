using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductManagement.Web.Data;
using ProductManagement.Web.Interfaces;
using ProductManagement.Web.Repositories;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Web.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextTransaction _transaction;

        public EfUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(_context, _transaction);
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                return 1;
            }
            catch
            {
                await _transaction.RollbackAsync();
                return 0;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
