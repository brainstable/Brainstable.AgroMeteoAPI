﻿using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.LoggerService;
using Brainstable.AgroMeteoAPI.Repository;
using Brainstable.AgroMeteoAPI.Service;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Service.DataShaping;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

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
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
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

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
                services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseNpgsql(configuration.GetConnectionString("npgsqlConnection")));

        public static void ConfigureDataShapers(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<MeteoStationDto>, DataShaper<MeteoStationDto>>();
            services.AddScoped<IDataShaper<MeteoPointDto>, DataShaper<MeteoPointDto>>();
        }
    }
}
