using System.Data.Common;
using DataAccess.Exceptions;
using DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Implementation
{
    public class DataBase : IDataBase, ILogging
    {
        private readonly IServiceCollection _services;

        public DataBase(IServiceCollection services)
        {
            _services = services;
        }

        public ILogging WithMsSql(string connectionString)
        {
            _services.AddScoped<IDataAccess>(provider => new DataAccessSql(connectionString));
            return this;
        }

        public ILogging WithOracleSql(string connectionString)
        {
            _services.AddScoped<IDataAccess>(provider => new DataAccessOracle(connectionString));
            return this;
        }

        public ILogging WithSybaseSql(string connectionString)
        {
            _services.AddScoped<IDataAccess>(provider => new DataAccessSybase(connectionString));
            return this;
        }

        public ILogging WithAllSql(DbConnection dbInstance)
        {
            if (dbInstance?.ConnectionString == default)
                throw new EmptyConnectionStringException("dbInstance connection can not be null or empty");
            
            _services.AddScoped<IDataAccess>(provider => new DataAccessAll(dbInstance));
            return this;
        }

        public void AndLog()
        {

        }

    }
}