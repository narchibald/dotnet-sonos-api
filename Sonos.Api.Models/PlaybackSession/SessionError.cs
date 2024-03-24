namespace Sonos.Api.Models.PlaybackSession
{
    using System.Text.Json.Serialization;

    public class SessionError
    {
        [JsonConstructor]
        public SessionError(string errorCode, string reason, string sessionId)
        {
            this.ErrorCode = errorCode;
            this.Reason = reason;
            this.SessionId = sessionId;
        }

        public string ErrorCode { get; }

        public string Reason { get; }

        public string SessionId { get; }
    }
}