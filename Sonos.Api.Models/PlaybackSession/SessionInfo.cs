using System.Text.Json.Serialization;

namespace Sonos.Api.Models.PlaybackSession
{
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