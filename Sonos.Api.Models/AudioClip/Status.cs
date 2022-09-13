namespace Sonos.Api.Models.AudioClip
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Status
    {
        [JsonConstructor]
        public Status(List<Clip> clips)
        {
            this.Clips = clips;
        }

        [JsonPropertyName("audioClips")]
        public List<Clip> Clips { get; }
    }
}