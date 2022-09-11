namespace Sonos.Api;

using Sonos.Api.Models;

public class Subscriptions : ISubscriptions
{
    private IControlRequest controlRequest;
    private Dictionary<EventType, HashSet<string>> activeSubscriptions = new ();

    public Subscriptions(IControlRequest controlRequest)
    {
        this.controlRequest = controlRequest;
    }

    public async Task Subscribe(EventType eventType, Player player)
    {
        var path = PlayerPath(eventType, player);

        await this.controlRequest.Execute(HttpMethod.Post, path);
        AddSubscription(eventType, path);
    }

    public async Task Unsubscribe(EventType eventType, Player player)
    {
        var path = PlayerPath(eventType, player);

        await this.controlRequest.Execute(HttpMethod.Delete, path);
        RemoveSubscription(eventType, path);
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var sub in this.activeSubscriptions.SelectMany(x => x.Value))
        {
            await this.controlRequest.Execute(HttpMethod.Delete, sub);
        }
    }

    private void AddSubscription(EventType eventType, string path)
    {
        if (!this.activeSubscriptions.TryGetValue(eventType, out var registrations))
        {
            registrations = new HashSet<string>();
        }

        if (!registrations.Contains(path))
        {
            registrations.Add(path);
        }
    }

    private void RemoveSubscription(EventType eventType, string path)
    {
        if (!this.activeSubscriptions.TryGetValue(eventType, out var registrations))
        {
            registrations = new HashSet<string>();
        }

        registrations.Remove(path);
    }

    private string PlayerPath(EventType eventType, Player player)
    {
        return eventType switch
        {
            EventType.AudioClip => $"/v1/players/{player.Id}/audioClip/subscription",
            EventType.PlayerVolume => $"/v1/players/{player.Id}/playerVolume/subscription",
            _ => throw new NotSupportedException($"Event type {eventType} not supported for player")
        };
    }
}