namespace Sonos.Api;

public interface IConfiguration
{
    string ClientId { get; }

    string ClientSecret { get; }

    Uri RedirectUri { get; }

    string AppId { get; }

    string AccessTokenPath { get; }
}