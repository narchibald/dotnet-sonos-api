namespace Sonos.Api.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSonosServices(this IServiceCollection services)
    {
        services.AddSingleton<IAccess, Access>();
        services.AddSingleton<IControl, Control>();
        services.AddSingleton<IControlRequest, ControlRequest>();
        services.AddSingleton<ISubscriptions, Subscriptions>();
        services.AddSingleton<Eventer>();
        services.AddSingleton<IEventer>(sc => sc.GetRequiredService<Eventer>());
        services.AddSingleton<IEventHandler>(sc => sc.GetRequiredService<Eventer>());
        services.AddHttpClient();

        return services;
    }
}
