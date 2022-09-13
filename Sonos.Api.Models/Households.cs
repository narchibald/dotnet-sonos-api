namespace Sonos.Api.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Households
    {
        [JsonConstructor]
        public Households(List<Household> items)
        {
            this.Items = items;
        }

        [JsonPropertyName("households")]
        public List<Household> Items { get; }
    }
}