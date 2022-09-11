namespace Sonos.Api.Models.PlaybackSession;

public record SessionError(string ErrorCode, string Reason, string SessionId);