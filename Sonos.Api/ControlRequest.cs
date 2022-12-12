namespace Sonos.Api;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ControlRequest : IControlRequest
{
    private const string BaseUri = "https://api.ws.sonos.com/control/api";
    private readonly IAccess access;
    private readonly IHttpClientFactory httpClientFactory;

    public ControlRequest(IAccess access, IHttpClientFactory httpClientFactory)
    {
        this.access = access;
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<T?> Execute<T>(HttpMethod method, string path, object? body = null)
    {
        var response = await ExecuteRequest(method, path, body);
        return await DeserializeFromResponse<T>(response);
    }

    public async Task Execute(HttpMethod method, string path, object? body = null)
    {
        var response = await ExecuteRequest(method, path, body);
        response.EnsureSuccessStatusCode();
    }

    private async Task<HttpResponseMessage> ExecuteRequest(HttpMethod method, string path, object? body = null)
    {
        var message = await MakeRequestMessage(method, MakeUri(path));
        if (body != null)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var json = JsonSerializer.Serialize(body, options);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var client = this.httpClientFactory.CreateClient();
        return await client.SendAsync(message);
    }

    private Uri MakeUri(string path)
    {
        if (!path.StartsWith('/'))
        {
            path = '/' + path;
        }

        return new Uri(BaseUri + path);
    }

    private Task<HttpRequestMessage> MakeRequestMessage(HttpMethod method, Uri uri)
    {
        HttpRequestMessage message = new (method, uri);
        return this.access.AddHeader(message);
    }

    private async Task<T?> DeserializeFromResponse<T>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        };

        await using var stream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(stream, jsonOptions);
    }
}