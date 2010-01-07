using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InRetail.Procurement.UserInterface.Purchasing
{
    public class PurchaseOrder
    {
        
        public PurchaseOrder()
        {
            Lines=new ObservableCollection<PurchaseOrderLine>();
        }
        
        public Supplier Supplier { get; set; }

        public WhereHouse WhereHouse { get; set; }

        public IList<PurchaseOrderLine> Lines { get; set; }

        public Guid Id { get; set; }
    }
}