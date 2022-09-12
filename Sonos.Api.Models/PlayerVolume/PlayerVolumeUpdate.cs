namespace Sonos.Api.Models.PlayerVolume
{
    public class PlayerVolumeUpdate
    {
        public PlayerVolumeUpdate(int volume, bool? muted)
        {
            this.Volume = volume;
            this.Muted = muted;
        }

        public int Volume { get; }

        public bool? Muted { get; }
    }
}