using System;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation
{
    public class PurchaseOrderLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}