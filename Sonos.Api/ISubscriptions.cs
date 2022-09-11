namespace Sonos.Api;

using Sonos.Api.Models;

public interface ISubscriptions : IAsyncDisposable
{
    Task Subscribe(EventType eventType, Player player);

    Task Unsubscribe(EventType eventType, Player player);
}