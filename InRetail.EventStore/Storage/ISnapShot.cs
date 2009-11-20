using System;
using InRetail.EventStore.Storage.Memento;

namespace InRetail.EventStore.Storage
{
    public interface ISnapShot
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}