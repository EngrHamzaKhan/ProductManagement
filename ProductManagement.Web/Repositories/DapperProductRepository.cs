using Dapper;
using ProductManagement.Web.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ProductManagement.Web.Repositories
{
    public class DapperRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _transaction;

        // Constructor to accept the transaction
        public DapperRepository(IDbConnection dbConnection, IDbTransaction transaction)
        {
            _dbConnection = dbConnection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<T>("sp_GetAll" + typeof(T).Name + "s",
                commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>("sp_Get" + typeof(T).Name + "ById",
                new { Id = id }, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public async Task AddAsync(T entity)
        {
            var parameters = entity.GetType().GetProperties()
                .Where(p => p.Name != "Id")
                .ToDictionary(p => p.Name, p => p.GetValue(entity));
            await _dbConnection.ExecuteAsync("sp_Insert" + typeof(T).Name,
                parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public async Task UpdateAsync(T entity)
        {
            var parameters = entity.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(entity));
            await _dbConnection.ExecuteAsync("sp_Update" + typeof(T).Name,
                parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await _dbConnection.ExecuteAsync("sp_Delete" + typeof(T).Name,
                new { Id = id }, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }
    }
}
