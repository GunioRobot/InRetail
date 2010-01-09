using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace InRetail.EntityPresentation
{
    public static class EntityBaseExtensions
    {
        public static string Property<T, TProperty>(this T notifier, Expression<Func<T, TProperty>> expression)
        where T : INotifyPropertyChanged
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }
    }
}