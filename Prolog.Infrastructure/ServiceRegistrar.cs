using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prolog.Abstractions.Services;
using Prolog.Application.Orders;
using Prolog.Domain;
using Prolog.Infrastructure.DaDataServices;
using Prolog.Infrastructure.HttpClients;

namespace Prolog.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterExternalInfrastructureServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        services.AddHttpClient<MapBoxHttpClient>();
        services.AddTransient<IDaDataService, DaDataService>();
        services.AddTransient<IMapBoxService, MapBoxService>();
        services.AddSingleton<IPrologBotService, PrologTelegramBotService>(x =>
            new PrologTelegramBotService(serviceProvider.GetRequiredService<IConfiguration>(),
                serviceProvider.GetRequiredService<ApplicationDbContext>(), new OrderMapper()));
        return services;
    }

    public static void ListenBot(this IApplicationBuilder app)
    {
        var telegramBot = app.ApplicationServices.GetRequiredService<IPrologBotService>();
    }
}