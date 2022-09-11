namespace Sonos.Api;

public interface IEventer
{
    event Action<Sonos.Api.Models.Events.@Event> @Event;
}