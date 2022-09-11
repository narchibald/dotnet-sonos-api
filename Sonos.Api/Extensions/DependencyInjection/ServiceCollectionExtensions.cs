namespace Sonos.Api.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSonosServices(this IServiceCollection services)
    {
        services.AddSingleton<AuthorizationResult>();
        services.AddSingleton<IAuthorizationResult>(sc => sc.GetRequiredService<AuthorizationResult>());
        services.AddSingleton<IAuthorizationResultReceiver>(sc => sc.GetRequiredService<AuthorizationResult>());
        services.AddSingleton<IAccess, Access>();
        services.AddSingleton<IControl, Control>();
        services.AddSingleton<IControlRequest, ControlRequest>();
        services.AddSingleton<ISubscriptions, Subscriptions>();
        services.AddSingleton<Eventer>();
        services.AddSingleton<IEventer>(sc => sc.GetRequiredService<Eventer>());
        services.AddSingleton<IEventHandler>(sc => sc.GetRequiredService<Eventer>());

        return services;
    }
}
