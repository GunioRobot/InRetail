using System;
using System.Collections.Generic;

namespace InRetail.EventStore.Storage
{
    public interface IDomainEventStorage : ISnapShotStorage, ITransactional
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider eventProvider);
    }
}