using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InRetail.EntityPresentation;

namespace InRetail.Procurement.UserInterface.Purchasing
{
    public class PurchaseOrder : EntityBase
    {
        public PurchaseOrder()
        {
            Lines = new ObservableCollection<PurchaseOrderLine>();
        }

        public Supplier Supplier { get; set; }

        public WhereHouse WhereHouse { get; set; }

        public IList<PurchaseOrderLine> Lines { get; set; }

        public Guid Id { get; set; }
    }
}