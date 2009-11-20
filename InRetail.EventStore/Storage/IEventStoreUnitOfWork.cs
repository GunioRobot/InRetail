using System;
using InRetail.EventStore.Storage.Memento;

namespace InRetail.EventStore.Storage
{
    public interface IEventStoreUnitOfWork : IUnitOfWork
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new();
        void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new();
        void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new();
    }
}