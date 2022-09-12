namespace Sonos.Api.Models
{
    using System.Collections.Generic;

    public class Households
    {
        public Households(List<Household> items)
        {
            this.Items = items;
        }

        [System.Text.Json.Serialization.JsonPropertyName("households")]
        public List<Household> Items { get; }
    }
}