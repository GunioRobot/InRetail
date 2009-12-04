using System;
using System.Collections;


namespace InRetail.UiCore.Extensions
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


        public static bool Is<T>(this object target)
        {
            return typeof(T).IsAssignableFrom(target.GetType());
        }


    }
}