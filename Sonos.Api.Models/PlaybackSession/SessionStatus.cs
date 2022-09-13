using System.Text.Json.Serialization;

namespace Sonos.Api.Models.PlaybackSession
{
    public class SessionStatus
    {
        [JsonConstructor]
        public SessionStatus(string sessionState, string sessionId, bool sessionCreated, string customData)
        {
            this.SessionState = sessionState;
            this.SessionId = sessionId;
            this.SessionCreated = sessionCreated;
            this.CustomData = customData;
        }

        public string SessionState { get; }

        public string SessionId { get; }

        public bool SessionCreated { get; }

        public string CustomData { get; }
    }
}