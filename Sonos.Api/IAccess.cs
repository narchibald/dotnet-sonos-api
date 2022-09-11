namespace Sonos.Api;

using System.Net.WebSockets;

public interface IAccess
{
    Task<bool> ValidateToken();

    Task<HttpRequestMessage> AddHeader(HttpRequestMessage message);

    Task<ClientWebSocket> AddHeader(ClientWebSocket ws);

    Task GetAccessToken(IAuthorizationResult authorizationResult);

    bool VerifyEvent(string seqId, string sonosNamespace, string type, string targetType, string tragetValue, string signature);
}