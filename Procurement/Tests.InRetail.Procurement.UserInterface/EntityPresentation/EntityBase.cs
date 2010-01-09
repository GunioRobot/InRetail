using System.ComponentModel;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public abstract class EntityBase : INotifyPropertyChanged,IEntity
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            var changed = PropertyChanged;
            if (changed != null) changed(this, e);
        }

        
        public virtual string GetEntityScreenName()
        {
            return GetType().Name;
        }
    }
}