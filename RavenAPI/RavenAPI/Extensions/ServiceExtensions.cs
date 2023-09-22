using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using RavenDAL;
using RavenDAL.Data;

namespace RavenAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors( options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureMSSqlContext(this IServiceCollection services,IConfiguration config)
        {
            var connectionString = config["mssqlconnection:connectionString"];
            services.AddDbContext<RavenDBContext>(o => o.UseSqlServer(connectionString));
        }
    }
}
