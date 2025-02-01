using Dapper;
using ProductManagement.Web.Interfaces;
using ProductManagement.Web.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace ProductManagement.Web.UnitOfWork
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        public DapperUnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
            _transaction = _dbConnection.BeginTransaction();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new DapperRepository<T>(_dbConnection,_transaction);
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                _transaction.Commit();
                return await Task.FromResult(1);
            }
            catch
            {
                _transaction.Rollback();
                return await Task.FromResult(0);
            }
            finally
            {
                _transaction.Dispose();
                _dbConnection.Close();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbConnection?.Dispose();
        }
    }
}
