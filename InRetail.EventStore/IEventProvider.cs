using System;
using System.Collections.Generic;

namespace InRetail.EventStore
{
    public interface IEventProvider
    {
        void Clear();
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
        void UpdateVersion(int version);
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> GetChanges();
    }
}