namespace Sonos.Api;

public class AuthorizationResult : IAuthorizationResult
{
    public AuthorizationResult(string code, string state, string? error)
    {
        this.Code = code;
        this.State = state;
        this.Error = error;
    }

    public string Code { get; } = string.Empty;

    public string State { get; } = string.Empty;

    public string? Error { get; }

    public bool IsErrored => !string.IsNullOrWhiteSpace(this.ErrorReason);

    public string? ErrorReason { get;  }
}