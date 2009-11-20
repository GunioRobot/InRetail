using System;

namespace InRetail.UserInterface.Eventing
{
    public interface If<T>
    {
        object PublishTo(Action<T> action);
    }
}