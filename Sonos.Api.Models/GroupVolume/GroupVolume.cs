namespace Sonos.Api.Models.GroupVolume
{
    using System.Text.Json.Serialization;

    public class GroupVolume
    {
        [JsonConstructor]
        public GroupVolume(int volume, bool muted, bool @fixed)
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