using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.LoggerService;

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
    }
}
