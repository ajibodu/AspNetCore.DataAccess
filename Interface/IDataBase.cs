using System.Data.Common;

namespace DataAccess.Interface
{
    internal interface IDataBase
    {
        ILogging WithMsSql(string connectionString);
        ILogging WithOracleSql(string connectionString);
        ILogging WithSybaseSql(string connectionString);
        ILogging WithAllSql(DbConnection dbInstance);
    }
}