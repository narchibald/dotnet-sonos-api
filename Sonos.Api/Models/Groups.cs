namespace Sonos.Api.Models;

public class Groups
{
    [System.Text.Json.Serialization.JsonPropertyName("Groups")]
    public List<Group> SubGroups { get; init; } = new ();

    public List<Player> Players { get; init; } = new ();

    public bool Partial { get; init; }
}