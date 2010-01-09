using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Tests.InRetail.Procurement.AssertHelpers
{
    public static class TestEx
    {
        public static void ShouldBeNotifyProperty<T, TResult>(this T target, Expression<Func<T, TResult>> property, TResult value)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (property == null) throw new ArgumentNullException("property");

            var body = property.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("The specified expression does not reference a property.", "property");

            var propertyInfo = body.Member as PropertyInfo;

            if (propertyInfo == null)
                throw new ArgumentException("The specified expression does not reference a property.", "property");

            string propertyName = propertyInfo.Name;

            var propertyDescriptor = (from p in TypeDescriptor.GetProperties(target).Cast<PropertyDescriptor>()
                                      where string.Equals(p.Name, propertyName, StringComparison.Ordinal)
                                      select p)
                .Single();

            if (!propertyDescriptor.SupportsChangeEvents)
                throw new ArgumentException("The specified property does not support change events.", "property");

            PropertyInfo info = typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public).First(x => x.Name == propertyName);

            bool notified = false;
            target.FromPropertyChanged(property).Subscribe(x => notified = true);
            info.SetValue(target,value,null);
            
            if (!notified)
                throw new ArgumentException("The specified property does not support change events.", propertyDescriptor.Name);
        }
    }
}