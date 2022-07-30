using System.Data.Common;
using DataAccess.Exceptions;
using DataAccess.Implementation;
using DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class RegisterService
    {
        public static DataBase AddDataAccess(this IServiceCollection services, string connectionString = default)
            => new DataBase(services);
    }
    

    public class Test
    {
        public void Now()
        {
            var service = new ServiceCollection();
            service.AddDataAccess()
                .WithMsSql("")
                .AndLog();
        }
    }
}