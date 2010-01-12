using System;
using System.Collections.Generic;
using InRetail.EntityPresentation;
using NServiceBus;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    public class When_Building_Presentation_Model : Specification
    {
        private EntityPresentationModelBuilder builder;
        private PresentationModel model;
        private PurchaseOrder order;

        public override void Given()
        {
            order = new PurchaseOrder
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

            order.TitleToReturn = "Model Title";

            builder = new EntityPresentationModelBuilder();
        }

        public override void When()
        {
            model = builder.Build(order);
        }


        [It]
        public void Should_Have_Model_Title()
        {
            model.Title.ShouldEqual("Model Title");
        }

        [It]
        public void First_Part_Should_Be()
        {
            Part part = model.Parts[0];
            part.Title.ShouldEqual("Order Attributes");
           
            part.Fields[0].Label.ShouldEqual("Ref.");
            part.Fields[0].Value.ShouldEqual("PO001");
            
            part.Fields[1].Label.ShouldEqual("Order Date");
            part.Fields[1].Value.ShouldEqual(new DateTime(2010, 1, 12));

            //part.AssosiatedMessages[0]
        }
    }

    public interface IPurchaseOrderMessages : IMessage
    {
        Guid OrderId { get; set; }
    }

    public class ChangeOrderAttributes : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public string Ref { get; set; }
        public DateTime Date { get; set; }
    }

    public class ChangeSupplierAndUpdateSupplierPrices : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class ChangeSupplier : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class ChangeWareHouse : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public Guid WareHouseId { get; set; }
    }


    public class AddOrderLine : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
    
    public class ChangeOrderLine : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public Guid OrderLineId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public class RemoveOrderLine : IPurchaseOrderMessages
    {
        public Guid OrderId { get; set; }
        public Guid OrderLineId { get; set; }
    }

    public class Product : EntityBase { }
}