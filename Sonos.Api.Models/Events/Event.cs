namespace Sonos.Api.Models.Events
{
    using System;

    public class @Event
    {
        public @Event(string householdId, string @namespace, Type type, Target target, string seqId)
        {
            this.HouseholdId = householdId;
            this.@Namespace = @namespace;
            this.Type = type;
            this.Target = target;
            this.SeqId = seqId;
        }

        public string HouseholdId { get; }

        public string @Namespace { get; }

        public Type Type { get; }

        public Target Target { get; }

        public string SeqId { get; }
    }

    public class @Event<T> : @Event
    {
        public @Event(string householdId, string @namespace, Target target, string seqId, T data)
            : base(householdId, @namespace, typeof(T), target, seqId)
        {
            this.Data = data;
        }

        public T Data { get; }
    }
}