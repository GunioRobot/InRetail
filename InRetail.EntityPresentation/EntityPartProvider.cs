using System;
using System.Collections.Generic;
using System.ComponentModel;
using StructureMap.Util;

namespace InRetail.EntityPresentation
{
    public class EntityPartProvider<T> : IEntityPartProvider<T> where T : IEntity
    {
        public IEnumerable<EntityPartPresenter> GetEntityParts()
        {
            var cache = new Cache<string, EntityPartPresenter>(x => new EntityPartPresenter());
            var attributes = (Attribute[])typeof(T).GetCustomAttributes(typeof(EntityFieldAttribute), false);
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(T), attributes))
                cache[GetPartName(property)] = new EntityPartPresenter();
            return cache.GetAll();
        }


        private static string GetPartName(PropertyDescriptor property)
        {
            return ((EntityFieldAttribute)property.Attributes[typeof(EntityFieldAttribute)]).PartName;
        }
    }
}