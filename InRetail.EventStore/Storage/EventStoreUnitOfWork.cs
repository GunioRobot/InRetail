using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.Bus;
using InRetail.EventStore.Storage.Memento;

namespace InRetail.EventStore.Storage
{
    public class EventStoreUnitOfWork : IEventStoreUnitOfWork
    {
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly IIdentityMap _identityMap;
        private readonly IBus _bus;
        private readonly List<IEventProvider> _eventProviders;

        public EventStoreUnitOfWork(IDomainEventStorage domainEventStorage, IIdentityMap identityMap, IBus bus)
        {
            _domainEventStorage = domainEventStorage;
            _identityMap = identityMap;
            _bus = bus;
            _eventProviders = new List<IEventProvider>();
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            var aggregateRoot = new TAggregate();

            LoadSnapShotIfExists(id, aggregateRoot);

            loadRemainingHistoryEvents(id, aggregateRoot);

            RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            RegisterForTracking(aggregateRoot);
        }

        public void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            _eventProviders.Add(aggregateRoot);
            _identityMap.Add(aggregateRoot);
        }

        public void Commit()
        {
            _domainEventStorage.BeginTransaction();

            foreach (var eventProvider in _eventProviders)
            {
                _domainEventStorage.Save(eventProvider);
                _bus.Publish(eventProvider.GetChanges().Select(x => (object)x));
                eventProvider.Clear();
            }
            _eventProviders.Clear();

            _bus.Commit();
            _domainEventStorage.Commit();
        }

        public void Rollback()
        {
            _bus.Rollback();
            _domainEventStorage.Rollback();
            foreach (var eventProvider in _eventProviders)
            {
                _identityMap.Remove(eventProvider.GetType(), eventProvider.Id);
            }
            _eventProviders.Clear();
        }

        private void LoadSnapShotIfExists(Guid id, IOrginator aggregateRoot)
        {
            var snapShot = _domainEventStorage.GetSnapShot(id);
            if (snapShot == null)
                return;

            aggregateRoot.SetMemento(snapShot.Memento);
        }

        private void loadRemainingHistoryEvents(Guid id, IEventProvider aggregateRoot)
        {
            var events = _domainEventStorage.GetEventsSinceLastSnapShot(id);
            if (events.Count() > 0)
            {
                aggregateRoot.LoadFromHistory(events);
                return;
            }

            aggregateRoot.LoadFromHistory(_domainEventStorage.GetAllEvents(id));
        }
    }
}