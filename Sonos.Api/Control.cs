namespace Sonos.Api;

using Sonos.Api.Models;
using Sonos.Api.Models.PlayerVolume;
using System;

public class Control : IControl
{
    private readonly IControlRequest controlRequest;
    private readonly IConfiguration configuration;
    private readonly Dictionary<string, HouseholdGroups> groups = new ();
    private readonly SemaphoreSlim refreshLock = new (1, 1);
    private Dictionary<string, List<Player>> clipPlayers = new ();
    private Households? houseHolds;

    public Control(IControlRequest controlRequest, IConfiguration configuration)
    {
        this.controlRequest = controlRequest;
        this.configuration = configuration;
    }

    public IReadOnlyList<Household> Households => this.houseHolds?.Items ?? new List<Household>();

    public HouseholdGroups GetHouseholdGroups(string householdId)
    {
        if (this.groups.TryGetValue(householdId, out var grps))
        {
            return grps;
        }

        return new HouseholdGroups();
    }

    public IReadOnlyList<Player> GetHouseholdPlayers(string householdId)
    {
        if (this.groups.TryGetValue(householdId, out var grps))
        {
            return grps.Players;
        }

        return new List<Player>();
    }

    public IReadOnlyList<Player> GetHouseholdClipAudioPlayers(string householdId)
    {
        return this.GetHouseholdPlayers(householdId).Where(p => p.Capabilities.Contains("AUDIO_CLIP")).ToList();
    }

    public async Task Refresh()
    {
        await this.refreshLock.WaitAsync();
        try
        {
            this.houseHolds = await this.GetHouseHolds();
            if (this.houseHolds is null)
            {
                return;
            }

            this.groups.Clear();
#if NET6_0_OR_GREATER
            await Parallel.ForEachAsync(
                this.houseHolds.Items,
                async (h, c) =>
                    {
                        var groups = await this.GetHouseHoldGroups(h);
                        this.groups.Add(h.Id, groups ?? new HouseholdGroups());
                    });
#else
            foreach (var h in this.houseHolds.Items)
            {
                var groups = await this.GetHouseHoldGroups(h);
                this.groups.Add(h.Id, groups ?? new HouseholdGroups());
            }
#endif
        }
        finally
        {
            this.refreshLock.Release();
        }
    }

    public async Task PlayAudioClip(Player player, Uri audioClipUri, string name)
    {
        var body = new { streamUrl = audioClipUri.ToString(), name, appId = this.configuration.AppId };
        await this.controlRequest.Execute(HttpMethod.Post, $"/v1/players/{player.Id}/audioClip", body);
    }

    public Task<PlayerVolume?> GetPlayerVolume(Player player)
    {
        return this.controlRequest.Execute<PlayerVolume?>(HttpMethod.Get, $"/v1/players/{player.Id}/playerVolume");
    }

    public Task SetPlayerVolume(Player player, PlayerVolumeUpdate update)
    {
        return this.controlRequest.Execute<PlayerVolume?>(HttpMethod.Post, $"/v1/players/{player.Id}/playerVolume", update);
    }

    private Task<HouseholdGroups?> GetHouseHoldGroups(Household household) => this.controlRequest.Execute<HouseholdGroups?>(HttpMethod.Get, $"v1/households/{household.Id}/groups");

    private Task<Households?> GetHouseHolds() => this.controlRequest.Execute<Households?>(HttpMethod.Get, $"v1/households");
}