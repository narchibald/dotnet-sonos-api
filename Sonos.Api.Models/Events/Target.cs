namespace Sonos.Api.Models.Events
{
    using System.Text.Json.Serialization;

    public class Target
    {
        [JsonConstructor]
        public Target(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public string Type { get; }

        public string Value { get; }
    }
}