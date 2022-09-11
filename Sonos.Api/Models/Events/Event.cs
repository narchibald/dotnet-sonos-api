namespace Sonos.Api.Models.Events;

public record @Event(string HouseholdId, string @Namespace, Type Type, Target Target, string SeqId);

public record @Event<T>(string HouseholdId, string @Namespace, Target Target, string SeqId, T Data) : @Event(HouseholdId, @Namespace, typeof(T), Target, SeqId);