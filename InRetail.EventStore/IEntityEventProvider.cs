using System;
using System.Collections.Generic;

namespace InRetail.EventStore
{
    public interface IEntityEventProvider
    {
        void Clear();
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
        void HookUpVersionProvider(Func<int> versionProvider);
        IEnumerable<IDomainEvent> GetChanges();
        Guid Id { get; }
    }
}