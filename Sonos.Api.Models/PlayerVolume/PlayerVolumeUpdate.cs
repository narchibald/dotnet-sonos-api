namespace Sonos.Api.Models.PlayerVolume
{
    using System.Text.Json.Serialization;

    public class PlayerVolumeUpdate
    {
        [JsonConstructor]
        public PlayerVolumeUpdate(int volume, bool? muted)
        {
            this.Volume = volume;
            this.Muted = muted;
        }

        public int Volume { get; }

        public bool? Muted { get; }
    }
}