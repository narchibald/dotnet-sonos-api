namespace Sonos.Api.Models.AudioClip
{
    using System.Collections.Generic;

    public class Status
    {
        public Status(List<Clip> clips)
        {
            this.Clips = clips;
        }

        public List<Clip> Clips { get; }
    }
}