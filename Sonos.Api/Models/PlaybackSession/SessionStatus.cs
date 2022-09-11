namespace Sonos.Api.Models.PlaybackSession;

public record SessionStatus(string SessionState, string SessionId, bool SessionCreated, string CustomData);