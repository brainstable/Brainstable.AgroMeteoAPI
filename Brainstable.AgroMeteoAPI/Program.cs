using System.Security.Cryptography.Xml;
using Brainstable.AgroMeteoAPI;
using Brainstable.AgroMeteoAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureQors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AgroMeteoAPI",
        Version = "v1",
    });
});

builder.Services.AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddApplicationPart(typeof(Brainstable.AgroMeteoAPI.Presentation.AssemblyReference)
    .Assembly)
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters()); ;


var app = builder.Build();

//var logger = app.Services.GetRequiredService<ILoggerManager>();
//app.ConfigureExeptionHandler(logger);
app.UseExceptionHandler(options => { });

if (app.Environment.IsProduction())
{
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "AgroMeteoAPI v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
