using System;

namespace InRetail.UserInterface.Eventing
{
    public interface IEventAggregator
    {
        // Sending messages
        void SendMessage<T>(T message);
        void SendMessage<T>() where T : new();

        // This method sounded cool, but has been somewhat awkward
        // in real usage
        void SendMessage<T>(Action<T> action) where T : class;

        // Explicit registration
        void AddListener(object listener);
        void RemoveListener(object listener);

        // Filtered registration, experimental
        If<T> If<T>(Func<T, bool> filter);
    }
}