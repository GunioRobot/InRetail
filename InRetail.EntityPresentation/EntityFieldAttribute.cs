using System;

namespace InRetail.EntityPresentation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class EntityFieldAttribute : Attribute
    {
        public string PartName { get; set; }
    }
}