namespace Sonos.Api;

public class AuthorizationResult : IAuthorizationResult, IAuthorizationResultReceiver
{
    private readonly CancellationTokenSource waitToken = new ();
    private static int count;

    public AuthorizationResult()
    {
        Interlocked.Increment(ref count);
    }

    public string Code { get; private set; } = string.Empty;

    public string State { get; private set; } = string.Empty;

    public bool IsErrored => !string.IsNullOrWhiteSpace(this.ErrorReason);

    public string? ErrorReason { get; private set; }

    public async Task WaitForResult()
    {
        try
        {
            await Task.Delay(TimeSpan.FromMilliseconds(-1), waitToken.Token);
        }
        catch (Exception)
        {
        }
    }

    public void SetResult(string code, string state, string? error)
    {
        this.Code = code;
        this.State = state;
        this.ErrorReason = error;
        waitToken.Cancel();
    }
}