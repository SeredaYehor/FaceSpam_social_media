using FaceSpam.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FaceSpam.DAL.Injection;

public static class DataBaseLayerInjection 
{
    public static void AddDataBaseLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataBaseContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("SqlDatabase"),
                b => b.MigrationsAssembly(typeof(DataBaseContext).Assembly.FullName));
        });
    }
}