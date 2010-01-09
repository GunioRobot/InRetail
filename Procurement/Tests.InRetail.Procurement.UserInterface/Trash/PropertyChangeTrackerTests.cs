using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using InRetail.EntityPresentation;
using InRetail.UiCore.Extensions;
using Tests.InRetail.Procurement.EntityPresentation;
using Xunit;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement
{
 

    public class PropertyChangeTrackerTests
    {
        [Fact]
        public void CanTrackChanged()
        {
            var model = new Model();
            var tracker = new PropertyChangeTracker(model);
            model.Property1 = "Acho";
            model.Property1 = "Bacho";
            model.Property2 = 1;
            
            IEnumerable<string> props = tracker.GetChangedProperties();

            List<string> list = props.ToList();
            list[0].ShouldEqual("Property1");
            list[1].ShouldEqual("Property2");
        }
    }

    public class PropertyChangeTracker
    {
        private readonly Model _model;

        
        private readonly IList<string> _changedProperies;

        public PropertyChangeTracker(Model model)
        {
            _model = model;
            _changedProperies = new List<string>();
            _model.PropertyChanged += (s, e) => onPropertyChanged(e.PropertyName);
            
        }

        public IEnumerable<string> GetChangedProperties()
        {
            return _changedProperies;
        }

        private void onPropertyChanged(string propertyName)
        {
            if (!_changedProperies.Contains(propertyName))
                _changedProperies.Add(propertyName);
        }
    }

    public class Model : EntityBase
    {
        private string _property1;
        private int _property2;

        public string Property1
        {
            get { return _property1; }
            set
            {
                _property1 = value;
                NotifyPropertyChanged(this.Property(x=>x.Property1));
            }
        }

        public int Property2
        {
            get { return _property2; }
            set
            {
                _property2 = value;
                NotifyPropertyChanged(this.Property(x => x.Property2));
            }
        }

        
    }


    public static class ObservableEx
    {
        public static IObservable<TResult> FromPropertyChanged<T, TResult>(this T target, Expression<Func<T, TResult>> property)
        {
            return FromPropertyChangedCore(target, property);
        }

        private static IObservable<TResult> FromPropertyChangedCore<T, TResult>(T target, Expression<Func<T, TResult>> property)
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

            var getter = property.Compile();

            return from e in Observable.FromEvent<EventHandler, EventArgs>(
                       d => d.Invoke,
                       h => propertyDescriptor.AddValueChanged(target, h),
                       h => propertyDescriptor.RemoveValueChanged(target, h))
                   select getter(target);
        }



    }
}