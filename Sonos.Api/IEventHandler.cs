namespace Sonos.Api;

public interface IEventHandler
{
    void RaiseEvent(string householdId, string @namespace, string type, string targetType, string targetValue, string seqId, string body);
}