using System;
using System.Collections.Generic;
using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;

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
        public string TitleToReturn;

        public override string GetEntityScreenName()
        {
            return TitleToReturn;
        }

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

        public static PurchaseOrder MakeValidOrder()
        {
            return new PurchaseOrder
            {
                Ref = "PO001",
                Date = new DateTime(2010, 1, 12),
                Supplier = new Supplier() { Name = "Elgar" },
                WhareHouse = new Warehouse() { Name = "Lutecia Stock" },
                OrderLines = new List<PurchaseOrderLine>()
                                             {
                                                 new PurchaseOrderLine()
                                                     {Product = new Product(), Quantity = 2, Price = 25},
                                                 new PurchaseOrderLine()
                                                     {Product = new Product(), Quantity = 1, Price = 50}
                                             },
                Total = 100,
            };
        }
    }
}