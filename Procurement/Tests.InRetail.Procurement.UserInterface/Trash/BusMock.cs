using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;

namespace Tests.InRetail.Procurement
{
    
    public class BusMock : IBus
    {
        public IList<IMessage> SentMessages = new List<IMessage>();
        public T CreateInstance<T>() where T : IMessage
        {
            throw new NotImplementedException();
        }

        public T CreateInstance<T>(Action<T> action) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public object CreateInstance(Type messageType)
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(params T[] messages) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(Action<T> messageConstructor) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Type messageType)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>() where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Type messageType, Predicate<IMessage> condition)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Predicate<T> condition) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(Type messageType)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T>() where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void SendLocal(params IMessage[] messages)
        {
            throw new NotImplementedException();
        }

        public void SendLocal<T>(Action<T> messageConstructor) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public ICallback Send(params IMessage[] messages)
        {
            messages.Run(SentMessages.Add);
            return null;
        }

        public ICallback Send<T>(Action<T> messageConstructor) where T : IMessage
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            messageConstructor(instance);
            SentMessages.Add(instance);
            return null;
        }

        public ICallback Send(string destination, params IMessage[] messages)
        {
            throw new NotImplementedException();
        }

        public ICallback Send<T>(string destination, Action<T> messageConstructor) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Send(string destination, string correlationId, params IMessage[] messages)
        {
            throw new NotImplementedException();
        }

        public void Send<T>(string destination, string correlationId, Action<T> messageConstructor) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Reply(params IMessage[] messages)
        {
            throw new NotImplementedException();
        }

        public void Reply<T>(Action<T> messageConstructor) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public void Return(int errorCode)
        {
            throw new NotImplementedException();
        }

        public void HandleCurrentMessageLater()
        {
            throw new NotImplementedException();
        }

        public void DoNotContinueDispatchingCurrentMessageToHandlers()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> OutgoingHeaders
        {
            get { throw new NotImplementedException(); }
        }

        public IMessageContext CurrentMessageContext
        {
            get { throw new NotImplementedException(); }
        }
    }
}