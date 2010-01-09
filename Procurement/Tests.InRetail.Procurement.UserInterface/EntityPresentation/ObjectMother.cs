using System;
using System.Collections.Generic;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public static class ObjectMother
    {
        public static PurchaseOrder Make_Vaild_PurchaseOrder()
        {
            return new PurchaseOrder
                            {
                                Date = new DateTime(2010, 1, 8),
                                Ref = "OR001",
                                Supplier = new Supplier() {Name = "Elgar"},
                                WhareHouse = new Warehouse() {Name = "LuteciaStock"},
                                Total = 100,
                                OrderLines =
                                    new List<PurchaseOrderLine>() {new PurchaseOrderLine(), new PurchaseOrderLine()}
                            };
        }
    }
}