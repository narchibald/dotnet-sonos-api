﻿namespace Sonos.Api;

using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

public class Access : IAccess
{
    private readonly string acessTokenPath;
    private readonly IConfiguration configuration;
    private TokenData? data;

    public Access(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.acessTokenPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), this.configuration.AccessTokenPath, "sonos-access.key");
    }

    public async Task<bool> ValidateToken()
    {
        if (data is null)
        {
            await LoadToken();
        }

        if (data != null)
        {
            var expiryTime = data.FetchTime + TimeSpan.FromSeconds(data.Token.ExpiresIn);
            if (expiryTime < DateTimeOffset.Now)
            {
                await RefreshAccessToken();
            }
        }

        return data != null;
    }

    public async Task<HttpRequestMessage> AddHeader(HttpRequestMessage message)
    {
        // Validate the token is good this function will refresh it if it is expired
        await ValidateToken();
        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(data.Token.TokenType, data.Token.AccessToken);
        return message;
    }

    public async Task<ClientWebSocket> AddHeader(ClientWebSocket ws)
    {
        // Validate the token is good this function will refresh it if it is expired
        await ValidateToken();
        ws.Options.SetRequestHeader("Authorization", $"{data.Token.TokenType} {data.Token.AccessToken}");
        return ws;
    }

    public Task GetAccessToken(IAuthorizationResult authorizationResult)
    {
        return FetchAccessToken($"grant_type=authorization_code&code={authorizationResult.Code}&redirect_uri={Uri.EscapeDataString(this.configuration.RedirectUri.ToString())}");
    }

    public Task RefreshAccessToken()
    {
        return FetchAccessToken($"grant_type=refresh_token&refresh_token={data.Token.RefreshToken}", (m) =>
        {
            m.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded");
        });
    }

    public bool VerifyEvent(string seqId, string sonosNamespace, string type, string targetType, string targetValue, string signature)
    {
        var builder = new StringBuilder();
        builder.Append(seqId);
        builder.Append(sonosNamespace);
        builder.Append(type);
        builder.Append(targetType);
        builder.Append(targetValue);
        builder.Append(this.configuration.ClientId);
        builder.Append(this.configuration.ClientSecret);
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(builder.ToString()));
        var computedSignature = WebEncoders.Base64UrlEncode(hash);

        return computedSignature == signature;
    }

    private async Task SaveToken(OAuthToken? token)
    {
        this.data = token != null ? new TokenData { FetchTime = DateTimeOffset.Now, Token = token } : null;
        Directory.CreateDirectory(Path.GetDirectoryName(this.acessTokenPath)!);
        using FileStream fileStream = new (this.acessTokenPath, FileMode.Create);
        if (this.data != null)
        {
            await JsonSerializer.SerializeAsync(fileStream, this.data);
        }
    }

    private async Task LoadToken()
    {
        if (File.Exists(this.acessTokenPath))
        {
            using FileStream fileStream = new (this.acessTokenPath, FileMode.Open);
            this.data = await JsonSerializer.DeserializeAsync<TokenData>(fileStream);
        }
    }

    private async Task FetchAccessToken(string queryString, Action<HttpRequestMessage>? extraInitFunc = null)
    {
        UriBuilder uriBuilder = new (new Uri("https://api.sonos.com/login/v3/oauth/access"))
        {
            Query = queryString,
        };

        HttpRequestMessage message = new (HttpMethod.Post, uriBuilder.Uri);
        var basicAuth = $"{this.configuration.ClientId}:{this.configuration.ClientSecret}";
        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(basicAuth)));
        extraInitFunc?.Invoke(message);

        using HttpClient client = new ();
        using var response = await client.SendAsync(message);
        OAuthToken? token = null;
        if (response.IsSuccessStatusCode)
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            token = await JsonSerializer.DeserializeAsync<OAuthToken>(stream);
        }

        await SaveToken(token);
    }

    public class TokenData
    {
        [System.Text.Json.Serialization.JsonPropertyName("fetchTime")]
        public DateTimeOffset FetchTime { get; init; }

        [System.Text.Json.Serialization.JsonPropertyName("token")]
        public OAuthToken Token { get; init; }
    }

    public class OAuthToken
    {
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}