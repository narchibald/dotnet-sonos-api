namespace Sonos.Api.Models
{
    using System.Collections.Generic;

    public class Group
    {
        public Group(string id, string name, string coordinatorId, string playbackState, List<string> playerIds)
        {
            this.Id = id;
            this.Name = name;
            this.CoordinatorId = coordinatorId;
            this.PlaybackState = playbackState;
            this.PlayerIds = playerIds;
        }

        public string Id { get; }

        public string Name { get; }

        public string CoordinatorId { get; }

        public string PlaybackState { get; }

        public List<string> PlayerIds { get; }
    }
}