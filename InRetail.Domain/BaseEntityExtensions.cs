using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.EventStore;

namespace InRetail.Domain
{
    public static class BaseEntityExtensions
    {
        public static bool TryGetValueById<TEventProvider>(this IEnumerable<TEventProvider> list, Guid Id, out IEntityEventProvider baseEntity) where TEventProvider : IEntityEventProvider
        {
            baseEntity = list.Where(x => x.Id == Id).FirstOrDefault();
            return baseEntity != null;
        }
    }
}