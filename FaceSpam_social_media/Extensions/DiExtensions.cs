using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FaceSpam_social_media.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;

namespace FaceSpam_social_media.Extensions
{
    public static class DiExtensions
    {
        public static void AddSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MVCDBContext>(options =>
            {
                options.UseMySql(
                        configuration.GetConnectionString("SqlDatabase"), ServerVersion.Parse("8.0.25-mysql"),
                        b => b.MigrationsAssembly(typeof(MVCDBContext).Assembly.FullName))
                    /*.UseLazyLoadingProxies()*/;

            }
            );
        }
    }
}
