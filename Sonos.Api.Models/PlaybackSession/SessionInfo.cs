namespace Sonos.Api.Models.PlaybackSession
{
    using System.Text.Json.Serialization;

    public class SessionInfo
    {
        [JsonConstructor]
        public SessionInfo(bool suspended)
        {
            this.Suspended = suspended;
        }

        public bool Suspended { get; }
    }
}