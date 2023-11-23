using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using RavenDB.Data;
using Repository;
using Service;
using Service.Contracts;

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
            services.AddDbContext<RavenContext>(o => o.UseSqlServer(connectionString));
        }
        public static void ConfigureRepoWrapper(this IServiceCollection services)
        {
            services.AddTransient<IRepoWrapper, RepoWrapper>();
        }
        public static void ConfigureRepoManager(this IServiceCollection services)
        {
            services.AddScoped<IRepoManager, RepoManager>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
