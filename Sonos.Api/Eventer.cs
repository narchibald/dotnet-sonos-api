namespace Sonos.Api;

using System.Text.Json;
using System.Text.Json.Serialization;

public class Eventer : IEventer, IEventHandler
{
    public event Action<Sonos.Api.Models.Events.@Event>? @Event;

    void IEventHandler.RaiseEvent(string householdId, string @namespace, string type, string targetType, string targetValue, string seqId, string body)
    {
        if (@Event is null)
        {
            return;
        }

        var dataType = (@namespace, type) switch
        {
            ("playerVolume", "playerVolume") => typeof(Sonos.Api.Models.PlayerVolume.PlayerVolume),
            ("audioClip", "audioClip") => typeof(Sonos.Api.Models.AudioClip.Clip),
            ("audioClip", "audioClipStatus") => typeof(Sonos.Api.Models.AudioClip.Status),
            _ => null
        };

        if (dataType is null)
        {
            return;
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
        var data = JsonSerializer.Deserialize(body, dataType, jsonOptions);

        var eventDataType = typeof(Sonos.Api.Models.Events.@Event<>).MakeGenericType(dataType);

        var target = new Sonos.Api.Models.Events.Target(targetType, targetValue);
        // (string HouseholdId, string @Namespace, Target Target, string SeqId, T Data)
        var eventData = Activator.CreateInstance(eventDataType, householdId, @namespace, target, seqId, data);
        @Event.Invoke((Sonos.Api.Models.Events.@Event)eventData!);
    }

    private record DataTypeDef(string @Namespace, string Type);
}