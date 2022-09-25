namespace Sonos.Api;

public interface IAuthorizationResult
{
    string Code { get; }

    string State { get; }

    bool IsErrored { get; }

    string? ErrorReason { get; }
}