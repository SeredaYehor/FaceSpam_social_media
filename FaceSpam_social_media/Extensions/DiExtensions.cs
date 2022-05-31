using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Extensions
{
    public static class DiExtensions
    {
        public static void AddSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MVCDBContext>(options =>
            {
                /*options.UseMySql(
                         configuration.GetConnectionString("SqlDatabase"),
                         ServerVersion.Parse("8.0.25-mysql"),
                          b => b.MigrationsAssembly(typeof(MVCDBContext).Assembly.FullName));
                */
                /*options.UseMySql(
                            configuration.GetConnectionString("SqlDatabase"),
                            b => b.MigrationsAssembly(typeof(MVCDBContext).Assembly.FullName),
                            ServerVersion.Parse("8.0.25-mysql"))
                        .UseLazyLoadingProxies();*/
                options.UseMySql("server=127.0.0.1;user=root;password=password;database=mydb", ServerVersion.Parse("8.0.25-mysql"));
            }
            );
        }
    }
}