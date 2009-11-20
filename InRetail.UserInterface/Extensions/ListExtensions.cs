using System;
using System.Collections.Generic;

namespace InRetail.UserInterface.Extensions
{
    public static class ListExtensions
    {

        public static void Each<T>(this IList<T> list, Action<T> action)
        {
            if (list == null) return;

            foreach (T t in list)
            {
                action(t);
            }
        }
    }
}