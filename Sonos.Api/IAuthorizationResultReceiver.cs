namespace Sonos.Api;

public interface IAuthorizationResultReceiver
{
    void SetResult(string code, string state, string? error);
}