namespace Sonos.Api.Models.PlaybackSession
{
    public class SessionInfo
    {
        public SessionInfo(bool suspended)
        {
            this.Suspended = suspended;
        }

        public bool Suspended { get; }
    }
}