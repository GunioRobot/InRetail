using System;

namespace Tests.InRetail.Procurement.UserInterface.EntityModel
{
    public class PurchaseOrder : MyEntityBase
    {
        private DateTime _date;
        private string _ref;
        private Supplier _supplier;
        private Warehouse _whareHouse;
        private decimal _total;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; NotifyPropertyChanged(this.Property(x => x.Date)); }
        }

        public string Ref
        {
            get { return _ref; }
            set { _ref = value; NotifyPropertyChanged(this.Property(x => x.Ref)); }
        }

        public Supplier Supplier
        {
            get { return _supplier; }
            set { _supplier = value; NotifyPropertyChanged(this.Property(x => x.Supplier)); }
        }

        public Warehouse WhareHouse
        {
            get { return _whareHouse; }
            set { _whareHouse = value; NotifyPropertyChanged(this.Property(x => x.WhareHouse)); }
        }

        public decimal Total
        {
            get { return _total; }
            set { _total = value; NotifyPropertyChanged(this.Property(x => x.Total)); }
        }
    }

    public class Warehouse : MyEntityBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(this.Property(x => x.Name)); }
        }
    }

    public class Supplier : MyEntityBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(this.Property(x => x.Name)); }
        }
    }
}