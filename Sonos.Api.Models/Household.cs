namespace Sonos.Api.Models
{
    using System.Text.Json.Serialization;

    public class Household
    {
        [JsonConstructor]
        public Household(string id)
        {
            this.Id = id;
        }

        public string Id { get; }
    }
}
