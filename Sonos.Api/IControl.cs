namespace Sonos.Api;

using Sonos.Api.Models;

public interface IControl
{
    IReadOnlyList<Household> Households { get; }

    HouseholdGroups GetHouseholdGroups(string householdId);

    IReadOnlyList<Player> GetHouseholdPlayers(string householdId);

    IReadOnlyList<Player> GetHouseholdClipAudioPlayers(string householdId);

    Task Refresh();

    Task PlayAudioClip(Player player, Uri audioClipUri, string name);

    Task<Sonos.Api.Models.PlayerVolume.PlayerVolume?> GetPlayerVolume(Player player);

    Task SetPlayerVolume(Player player, Sonos.Api.Models.PlayerVolume.PlayerVolumeUpdate update);
}