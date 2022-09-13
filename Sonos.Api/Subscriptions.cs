namespace Sonos.Api;

using Sonos.Api.Models;
using Sonos.Api.Models.PlaybackSession;

public class Subscriptions : ISubscriptions
{
    private readonly IControlRequest controlRequest;
    private readonly Dictionary<EventType, HashSet<string>> activeSubscriptions = new ();

    public Subscriptions(IControlRequest controlRequest)
    {
        this.controlRequest = controlRequest;
    }

    private enum EventType
    {
        AudioClip,
        Favorites,
        Groups,
        GroupVolume,
        PlayBack,
        PlaybackMetadata,
        PlaybackSession,
        PlayerVolume,
        Playlists,
    }

    public Task Subscribe(HouseholdEventType eventType, Household household) =>
        Subscribe(eventType.ToString(), HouseholdPath(eventType, household));

    public Task Subscribe(GroupEventType eventType, Group group) =>
        Subscribe(eventType.ToString(), GroupPath(eventType, group));

    public Task Subscribe(PlayerEventType eventType, Player player) =>
        Subscribe(eventType.ToString(), PlayerPath(eventType, player));

    public Task Subscribe(SessionEventType eventType, SessionStatus session) =>
        Subscribe(eventType.ToString(), SessionPath(eventType, session));

    public bool HasSubscription(HouseholdEventType eventType, Household household) =>
        HasSubscription(eventType.ToString(), HouseholdPath(eventType, household));

    public bool HasSubscription(GroupEventType eventType, Group group) =>
        HasSubscription(eventType.ToString(), GroupPath(eventType, group));

    public bool HasSubscription(PlayerEventType eventType, Player player) =>
        HasSubscription(eventType.ToString(), PlayerPath(eventType, player));

    public bool HasSubscription(SessionEventType eventType, SessionStatus session) =>
        HasSubscription(eventType.ToString(), SessionPath(eventType, session));

    public Task Unsubscribe(HouseholdEventType eventType, Household household) =>
        Unsubscribe(eventType.ToString(), HouseholdPath(eventType, household));

    public Task Unsubscribe(GroupEventType eventType, Group group) =>
        Unsubscribe(eventType.ToString(), GroupPath(eventType, group));

    public Task Unsubscribe(PlayerEventType eventType, Player player) =>
        Unsubscribe(eventType.ToString(), PlayerPath(eventType, player));

    public Task Unsubscribe(SessionEventType eventType, SessionStatus session) =>
        Unsubscribe(eventType.ToString(), SessionPath(eventType, session));

    public async ValueTask DisposeAsync()
    {
        foreach (var sub in this.activeSubscriptions.SelectMany(x => x.Value))
        {
            await this.controlRequest.Execute(HttpMethod.Delete, sub);
        }
    }

    private async Task Subscribe(string eventName, string path)
    {
        var eventType = this.ResolveEventType(eventName);

        await this.controlRequest.Execute(HttpMethod.Post, path);
        AddSubscription(eventType, path);
    }

    private bool HasSubscription(string eventName, string path)
    {
        var eventType = this.ResolveEventType(eventName);
        if (!this.activeSubscriptions.TryGetValue(eventType, out var registrations))
        {
            return false;
        }

        return registrations.Contains(path);
    }

    private async Task Unsubscribe(string eventName, string path)
    {
        var eventType = this.ResolveEventType(eventName);

        await this.controlRequest.Execute(HttpMethod.Delete, path);
        RemoveSubscription(eventType, path);
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

    private string PlayerPath(PlayerEventType eventType, Player player)
    {
        return eventType switch
        {
            PlayerEventType.AudioClip => $"/v1/players/{player.Id}/audioClip/subscription",
            PlayerEventType.PlayerVolume => $"/v1/players/{player.Id}/playerVolume/subscription",
            _ => throw new NotSupportedException($"Event type {eventType} not supported for player")
        };
    }

    private string HouseholdPath(HouseholdEventType eventType, Household household)
    {
        return eventType switch
            {
                HouseholdEventType.Favorites => $"/v1/households/{household.Id}/favorites/subscription",
                HouseholdEventType.Groups => $"/v1/households/{household.Id}/groups/subscription",
                HouseholdEventType.Playlists => $"/v1/households/{household.Id}/playlists/subscription",
                _ => throw new NotSupportedException($"Event type {eventType} not supported for households")
            };
    }

    private string GroupPath(GroupEventType eventType, Group group)
    {
        return eventType switch
            {
                GroupEventType.GroupVolume => $"/v1/groups/{group.Id}/groupVolume/subscription",
                GroupEventType.PlayBack => $"/v1/groups/{group.Id}/playback/subscription",
                GroupEventType.PlaybackMetadata => $"/v1/groups/{group.Id}/playbackMetadata/subscription",
                _ => throw new NotSupportedException($"Event type {eventType} not supported for groups")
            };
    }

    private string SessionPath(SessionEventType eventType, SessionStatus sessionStatus)
    {
        return eventType switch
            {
                SessionEventType.PlaybackSession => $"/v1/playbackSessions/{sessionStatus.SessionId}/playbackSession/subscription",
                _ => throw new NotSupportedException($"Event type {eventType} not supported for session")
            };
    }

    private EventType ResolveEventType(string name) => Enum.Parse<EventType>(name);
}