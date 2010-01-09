using System;
using System.ComponentModel;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public class EntityFieldPresenter<T> :IEntityFieldPresenter, INotifyPropertyChanged
    {
        private string _title;
        private T _value;

        public EntityFieldPresenter(string title, IObservable<T> value)
        {
            Title = title;
            value.Subscribe(x => Value = x);
        }

        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value; InvokePropertyChanged(this.Property(x => x.Title));
            }
        }

        public T Value
        {
            get { return _value; }
            private set
            {
                _value = value; InvokePropertyChanged(this.Property(x => x.Value));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}