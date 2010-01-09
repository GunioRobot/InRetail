using System;
using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public class PurchaseOrder : EntityBase
    {
        private DateTime _date;
        private string _ref;
        private Supplier _supplier;
        private Warehouse _whareHouse;
        private decimal _total;
        private IList<PurchaseOrderLine> _orderLines;

        [EntityField(PartName = "Order Attributes")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; NotifyPropertyChanged(this.Property(x => x.Date)); }
        }

        [EntityField(PartName = "Order Attributes")]
        public string Ref
        {
            get { return _ref; }
            set { _ref = value; NotifyPropertyChanged(this.Property(x => x.Ref)); }
        }

        [EntityField(PartName = "Supplier")]
        public Supplier Supplier
        {
            get { return _supplier; }
            set { _supplier = value; NotifyPropertyChanged(this.Property(x => x.Supplier)); }
        }

        [EntityField(PartName = "Warehouse")]
        public Warehouse WhareHouse
        {
            get { return _whareHouse; }
            set { _whareHouse = value; NotifyPropertyChanged(this.Property(x => x.WhareHouse)); }
        }

        [EntityField(PartName = "Totals")]
        public decimal Total
        {
            get { return _total; }
            set { _total = value; NotifyPropertyChanged(this.Property(x => x.Total)); }
        }

        [EntityField(PartName = "OrderLines")]
        public IList<PurchaseOrderLine> OrderLines
        {
            get { return _orderLines; }
            set { _orderLines = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class EntityFieldAttribute : Attribute
    {
        public string PartName { get; set; }
    }
}