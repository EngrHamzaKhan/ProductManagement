using ProductManagement.Web.Interfaces;

namespace ProductManagement.Web.UnitOfWork
{
    // Not using right now later.
    public class UnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _databaseType;

        public UnitOfWorkFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        
        {
            _serviceProvider = serviceProvider;
            _databaseType = configuration["ORMType"]; // "EFCore" or "Dapper"
        }

        public IUnitOfWork Create()
        {
            return _databaseType == "Dapper"
                ? _serviceProvider.GetRequiredService<DapperUnitOfWork>()
                : _serviceProvider.GetRequiredService<EfUnitOfWork>();
        }
    }

}
