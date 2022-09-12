namespace Sonos.Api.Models.PlaybackSession
{
    public class SessionError
    {
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