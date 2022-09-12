namespace Sonos.Api.Models
{
    using System.Collections.Generic;

    public class Groups
    {
        public Groups()
        {
            this.SubGroups = new List<Group>();
            this.Players = new List<Player>();
        }

        public Groups(List<Group> subGroups, List<Player> players, bool partial)
        {
            this.SubGroups = subGroups;
            this.Players = players;
            this.Partial = partial;
        }

        [System.Text.Json.Serialization.JsonPropertyName("Groups")]
        public List<Group> SubGroups { get; }

        public List<Player> Players { get; }

        public bool Partial { get; }
    }
}