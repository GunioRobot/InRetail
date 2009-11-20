using System;
using System.Collections;
using StructureMap;

namespace InRetail.UserInterface.Extensions
{
    public static class TypeExtensions
    {
        public static void CallOn<T>(this object target, Action<T> action) where T : class
        {
            var subject = target as T;
            if (subject != null)
            {
                try
                {
                    action(subject);
                }
                catch (InvalidOperationException e)
                {
                    if (!e.ToString().Contains("The calling thread"))
                    {
                        throw;
                    }
                }
            }
        }

        public static void CallOnEach<T>(this IEnumerable enumerable, Action<T> action) where T : class
        {
            foreach (object o in enumerable)
            {
                o.CallOn(action);
            }
        }

        public static bool Implements<T>(this PluginTypeConfiguration configuration)
        {
            return typeof (T).IsAssignableFrom(configuration.PluginType);
        }

        public static bool Is<T>(this object target)
        {
            return typeof (T).IsAssignableFrom(target.GetType());
        }

        public static bool IsStartable(this PluginTypeConfiguration configuration)
        {
            return typeof (IStartable).IsAssignableFrom(configuration.PluginType);
        }

        public static T To<T>(this PluginTypeConfiguration configuration, IContainer container)
        {
            return (T) container.GetInstance(configuration.PluginType);
        }

        public static IStartable ToStartable(this PluginTypeConfiguration configuration, IContainer container)
        {
            return (IStartable) container.GetInstance(configuration.PluginType);
        }
    }
}