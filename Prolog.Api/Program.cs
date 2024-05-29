using NLog;
using NLog.Web;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.Http;
using Prolog.Api.Jobs.Configuration;
using Prolog.Api.Middlewares;
using Prolog.Api.StartupConfigurations;
using Prolog.Api.StartupConfigurations.Options;
using Prolog.Api.StartupConfigurations.Swagger;
using Prolog.Application;
using Prolog.Application.Hubs;
using Prolog.Domain;
using Prolog.Infrastructure;
using Prolog.Keycloak;
using Prolog.Keycloak.Configurations;
using Prolog.Services;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var logger = LogManager.Setup().LoadConfigurationFromXml("nlog.config").GetCurrentClassLogger();
logger.Info("Инициализация Prolog...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers();

    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSignalR();

    builder.Services.RegisterDataAccessServices(builder.Configuration, builder.Environment.IsDevelopment());
    builder.Services.RegisterUseCasesServices();
    builder.Services.RegisterDomainInfrastructureServices(builder.Configuration);

    builder.Services.ConfigureOptions<KeycloakConfigurationSetup>();
    builder.Services.ConfigureOptions<KeycloakScopesConfigurationSetup>();
    builder.Services.ConfigureOptions<MapBoxConfiguration>();

    builder.Services.RegisterExternalInfrastructureServices();
    builder.Services.RegisterKeycloakServices();
    builder.Services.ConfigureOwnSwagger();
    builder.Services.AddKeycloakConfig();

    builder.Services.RegisterQuartzJobs(builder.Configuration);

    builder.Services.AddScoped<ICurrentHttpContextAccessor, CurrentHttpContextAccessor>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            build =>
            {
                build
                    .AllowAnyOrigin()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("*");
            });

        options.AddPolicy("SignalR",
            build =>
            {
                build
                    .WithOrigins(builder.Configuration.GetSection("ClientUrl").Value!)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
    });

    var app = builder.Build();

    app.MapHealthChecks("/healthz");

    app.MigrateDb();

    app.UseCors("AllowAllOrigins");

    app.ConfigureSwaggerUi();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseMiddleware<ContextSetterMiddleware>();
    app.UseMiddleware<NLogRequestPostedBodyMiddleware>();

    app.MapControllers();
    app.ListenBot();
    app.MapHub<PrologHub>("/hub");

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Prolog остановлен из-за внутренней ошибки...");
    throw;
}
finally
{
    LogManager.Shutdown();
}
