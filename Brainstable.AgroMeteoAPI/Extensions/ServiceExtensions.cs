using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.LoggerService;
using Brainstable.AgroMeteoAPI.Repository;
using Brainstable.AgroMeteoAPI.Service;
using Brainstable.AgroMeteoAPI.Service.Contracts;

namespace Brainstable.AgroMeteoAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureQors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("QorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
    }
}
