namespace Sonos.Api;

using Sonos.Api.Models;
using Sonos.Api.Models.PlaybackSession;

public interface ISubscriptions : IAsyncDisposable
{
    Task Subscribe(HouseholdEventType eventType, Household household);

    Task Subscribe(GroupEventType eventType, Group group);

    Task Subscribe(PlayerEventType eventType, Player player);

    Task Subscribe(SessionEventType eventType, SessionStatus session);

    bool HasSubscription(HouseholdEventType eventType, Household household);

    bool HasSubscription(GroupEventType eventType, Group group);

    bool HasSubscription(PlayerEventType eventType, Player player);

    bool HasSubscription(SessionEventType eventType, SessionStatus session);

    Task Unsubscribe(HouseholdEventType eventType, Household household);

    Task Unsubscribe(GroupEventType eventType, Group group);

    Task Unsubscribe(PlayerEventType eventType, Player player);

    Task Unsubscribe(SessionEventType eventType, SessionStatus session);
}