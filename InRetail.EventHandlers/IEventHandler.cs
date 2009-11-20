using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InRetail.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        void Execute(TEvent theEvent);
    }
}
