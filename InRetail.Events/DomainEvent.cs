using System;
using InRetail.EventStore;

namespace InRetail.Events
{
    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        int IDomainEvent.Version { get; set; }
    }
}