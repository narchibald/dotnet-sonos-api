namespace Sonos.Api;

using Sonos.Api.Models;

public class Control : IControl
{
    private readonly IControlRequest controlRequest;
    private readonly IConfiguration configuration;
    private readonly Dictionary<string, Groups> groups = new ();
    private Dictionary<string, List<Player>> clipPlayers = new ();
    private Households? houseHolds;

    public Control(IControlRequest controlRequest, IConfiguration configuration)
    {
        this.controlRequest = controlRequest;
        this.configuration = configuration;
    }

    public IReadOnlyList<Household> Households => houseHolds?.Items ?? new List<Household>();

    public Groups GetHouseholdGroup(string householdId)
    {
        if (this.groups.TryGetValue(householdId, out var grps))
        {
            return grps;
        }

        return new Groups();
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

    public async Task Start()
    {
        this.houseHolds = await GetHouseHolds();
        if (this.houseHolds is null)
        {
            return;
        }

        await Parallel.ForEachAsync(this.houseHolds.Items, async (h, c) =>
        {
            var groups = await GetHouseHoldGroups(h);
            this.groups.Add(h.Id, groups ?? new Groups());
        });
    }

    public async Task PlayAudioClip(Player player, Uri audioClipUri, string name)
    {
        var body = new { streamUrl = audioClipUri.ToString(), name, appId = this.configuration.AppId };
        await controlRequest.Execute(HttpMethod.Post, $"/v1/players/{player.Id}/audioClip", body);
    }

    public Task<Sonos.Api.Models.PlayerVolume.PlayerVolume?> GetPlayerVolume(Player player)
    {
        return controlRequest.Execute<Sonos.Api.Models.PlayerVolume.PlayerVolume?>(HttpMethod.Get, $"/v1/players/{player.Id}/playerVolume");
    }

    public Task SetPlayerVolume(Player player, Sonos.Api.Models.PlayerVolume.PlayerVolumeUpdate update)
    {
        return controlRequest.Execute<Sonos.Api.Models.PlayerVolume.PlayerVolume?>(HttpMethod.Post, $"/v1/players/{player.Id}/playerVolume", update);
    }

    private Task<Groups?> GetHouseHoldGroups(Household household) => controlRequest.Execute<Groups?>(HttpMethod.Get, $"v1/households/{household.Id}/groups");

    private Task<Households?> GetHouseHolds() => controlRequest.Execute<Households?>(HttpMethod.Get, $"v1/households");
}