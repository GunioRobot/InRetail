using System;
using System.Collections.Generic;
using System.ComponentModel;
using StructureMap.Util;

namespace InRetail.EntityPresentation
{
    public class EntityPartProvider<T> : IEntityPartProvider<T> where T : IEntity
    {
        public IEnumerable<IPart> GetEntityParts()
        {
            //var cache = new Cache<string, _entityPartPresenter>(x => new _entityPartPresenter());
            //var attributes = (Attribute[])typeof(T).GetCustomAttributes(typeof(EntityFieldAttribute), false);
            //foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(T), attributes))
            //    cache[GetPartName(property)] = new _entityPartPresenter();
            //return cache.GetAll();
            throw new NotImplementedException();
        }


        private static string GetPartName(PropertyDescriptor property)
        {
            return ((EntityFieldAttribute)property.Attributes[typeof(EntityFieldAttribute)]).PartName;
        }
    }
}