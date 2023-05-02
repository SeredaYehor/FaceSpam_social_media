using FaceSpam.DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FaceSpam.DAL;

public static class DalInjection
{
    public static void AddDatabase(this IServiceCollection services, string connection)
    {
        services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(
                    connection,
                    b => b.MigrationsAssembly(typeof(DataBaseContext).Assembly.FullName));
            }
        );
    }
}