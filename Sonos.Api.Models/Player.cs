namespace Sonos.Api.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Player
    {
        [JsonConstructor]
        public Player(
            string id,
            string name,
            string websocketUrl,
            string softwareVersion,
            string apiVersion,
            string minApiVersion,
            bool isUnregistered,
            List<string> capabilities,
            List<string> deviceIds)
        {
            this.Id = id;
            this.Name = name;
            this.WebsocketUrl = websocketUrl;
            this.SoftwareVersion = softwareVersion;
            this.ApiVersion = apiVersion;
            this.MinApiVersion = minApiVersion;
            this.IsUnregistered = isUnregistered;
            this.Capabilities = capabilities;
            this.DeviceIds = deviceIds;
        }

        public string Id { get; }

        public string Name { get; }

        public string WebsocketUrl { get; }

        public string SoftwareVersion { get; }

        public string ApiVersion { get; }

        public string MinApiVersion { get; }

        public bool IsUnregistered { get; }

        public List<string> Capabilities { get; }

        public List<string> DeviceIds { get; }
    }
}