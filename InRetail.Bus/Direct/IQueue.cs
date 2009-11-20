using System;

namespace InRetail.Bus.Direct
{
    public interface IQueue
    {
        void Put(object item);
        void Pop(Action<object> popAction);
    }
}