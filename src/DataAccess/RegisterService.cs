using DataAccess.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class RegisterService
    {
        public static DataBase AddDataAccess(this IServiceCollection services, string connectionString = default)
            => new DataBase(services);
    }
    

    // public class Test
    // {
    //     public void Now()
    //     {
    //         var service = new ServiceCollection();
    //         service.AddDataAccess()
    //             .WithMsSql("")
    //             .AndLog();
    //     }
    // }
}