namespace Sonos.Api.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class HouseholdGroups
    {
        public HouseholdGroups()
        {
            this.Groups = new List<Group>();
            this.Players = new List<Player>();
        }

        [JsonConstructor]
        public HouseholdGroups(List<Group> groups, List<Player> players, bool partial)
        {
            this.Groups = groups;
            this.Players = players;
            this.Partial = partial;
        }

        public List<Group> Groups { get; }

        public List<Player> Players { get; }

        public bool Partial { get; }
    }
}