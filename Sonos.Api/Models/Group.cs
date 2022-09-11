namespace Sonos.Api.Models;

public record Group(string Id, string Name, string CoordinatorId, string PlaybackState, List<string> PlayerIds);