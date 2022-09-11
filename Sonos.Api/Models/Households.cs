namespace Sonos.Api.Models;

public class Households
{
    [System.Text.Json.Serialization.JsonPropertyName("households")]
    public List<Household> Items { get; init; } = new ();
}