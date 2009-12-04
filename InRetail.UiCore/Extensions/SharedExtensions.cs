using System;
using System.Collections.Generic;
using System.Linq;

namespace InRetail.UiCore.Extensions
{
    public static class SharedExtensions
    {
        public static T As<T>(this object target)
        {
            return (T) target;
        }
        public static T Configure<T>(this T target, Action<T> action)
        {
            action(target);
            return target;
        }
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        private static Func<T, bool> finderWithin<T, U>(Predicate<U> predicate) where U : class
        {
            return x =>
                       {
                           var u = x as U;
                           return u == null ? false : predicate(u);
                       };
        }

        public static U First<T, U>(this IEnumerable<T> enumerable, Predicate<U> predicate)
            where U : class
            where T : class
        {
            Func<T, bool> finder = finderWithin<T, U>(predicate);
            return enumerable.First(finder) as U;
        }

        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotEmpty(this string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        public static bool IsSame(this object target, object other)
        {
            return ReferenceEquals(target, other);
        }

        public static bool IsTrue(this string value)
        {
            return bool.Parse(value);
        }

        public static bool IsTrue(this object value)
        {
            return value is bool ? (bool) value : value.ToString().IsTrue();
        }

        public static string Join(this string[] array, string separator)
        {
            return string.Join(separator, array);
        }

        public static void MoveDown<T>(this IList<T> list, T subject)
        {
            if (ReferenceEquals(subject, list.LastOrDefault())) return;

            int index = list.IndexOf(subject);
            list.Remove(subject);
            list.Insert(index + 1, subject);
        }

        public static void MoveUp<T>(this IList<T> list, T subject)
        {
            int index = list.IndexOf(subject);
            if (index == 0) return;

            list.Remove(subject);
            list.Insert(index - 1, subject);
        }

        public static T Parse<T>(this string value)
        {
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public static string[] ToDelimitedArray(this string content)
        {
            return content.ToDelimitedArray(',');
        }

        public static string[] ToDelimitedArray(this string content, char delimiter)
        {
            string[] array = content.Split(delimiter);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Trim();
            }

            return array;
        }

        public static string ToFormat(this string template, params object[] parameters)
        {
            return string.Format(template, parameters);
        }

        public static int ToInt(this string stringValue)
        {
            return int.Parse(stringValue);
        }

        public static IEnumerable<T> WhereMatching<T, U>(this IEnumerable<T> enumerable, Predicate<U> predicate)
            where U : class
            where T : class
        {
            Func<T, bool> finder = finderWithin<T, U>(predicate);
            return enumerable.Where(finder);
        }
    }
}