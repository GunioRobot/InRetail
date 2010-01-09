using InRetail.EntityPresentation;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public class Warehouse : EntityBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(this.Property(x => x.Name)); }
        }
    }
}