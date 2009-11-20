using System;
using InRetail.UserInterface.Eventing;
using StructureMap;
using StructureMap.Interceptors;

namespace InRetail.UserInterface
{
    public class EventAggregatorInterceptor : TypeInterceptor
    {
        #region TypeInterceptor Members

        public object Process(object target, IContext context)
        {
            context.GetInstance<IEventAggregator>().AddListener(target);
            return target;
        }

        public bool MatchesType(Type type)
        {
            if (typeof (IHandler).IsAssignableFrom(type)) return false;

            return type.ImplementsInterfaceTemplate(typeof (IListener<>)) ||
                type.CanBeCastTo(typeof (ICloseable));
        }

        #endregion
    }
}