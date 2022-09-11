namespace Sonos.Api.Models;

public record Player(string Id, string Name, string WebsocketUrl, string SoftwareVersion, string ApiVersion, string MinApiVersion, bool IsUnregistered, List<string> Capabilities, List<string> DeviceIds);