namespace Sonos.Api;

public interface IControlRequest
{
    Task<T?> Execute<T>(HttpMethod method, string path, object? body = null);

    Task Execute(HttpMethod method, string path, object? body = null);
}