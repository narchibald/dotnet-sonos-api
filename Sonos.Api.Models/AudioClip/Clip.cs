namespace Sonos.Api.Models.AudioClip
{
    public class Clip
    {
        public Clip(
            string appId,
            string id,
            string name,
            ClipState status,
            ClipType? clipType = null,
            Priority? priority = null,
            string? errorCode = null)
        {
            this.AppId = appId;
            this.Id = id;
            this.Name = name;
            this.Status = status;
            this.ClipType = clipType;
            this.Priority = priority;
            this.ErrorCode = errorCode;
        }

        public string AppId { get; }

        public string Id { get; }

        public string Name { get; }

        public ClipState Status { get; }

        public ClipType? ClipType { get; }

        public Priority? Priority { get; }

        public string? ErrorCode { get; }
    }
}