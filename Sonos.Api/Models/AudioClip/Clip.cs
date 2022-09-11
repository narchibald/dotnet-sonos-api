namespace Sonos.Api.Models.AudioClip;

public record Clip(string AppId, string Id, string Name, ClipState Status)
{
    public ClipType? ClipType { get; init; }

    public Priority? Priority { get; init; }

    public string? ErrorCode { get; init; }
}