using InRetail.EntityPresentation;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public class Supplier : EntityBase
    {
        private string _name;
        public  string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(this.Property(x => x.Name)); }
        }

        #region Supports Testing

        public bool Equals(Supplier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._name, _name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Supplier)) return false;
            return Equals((Supplier) obj);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        #endregion

    }
}