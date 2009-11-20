using System;

namespace InRetail.EventStore.Storage
{
    public interface ISnapShotStorage
    {
        ISnapShot GetSnapShot(Guid entityId);
        void SaveShapShot(IEventProvider entity);
    }
}