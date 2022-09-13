namespace Sonos.Api.Models.PlayerVolume
{
    using System.Text.Json.Serialization;

    public class PlayerVolume
    {
        [JsonConstructor]
        public PlayerVolume(int volume, bool muted, bool @fixed)
        {
            this.Volume = volume;
            this.Muted = muted;
            this.Fixed = @fixed;
        }

        public int Volume { get; }

        public bool Muted { get; }

        public bool Fixed { get; }
    }
}