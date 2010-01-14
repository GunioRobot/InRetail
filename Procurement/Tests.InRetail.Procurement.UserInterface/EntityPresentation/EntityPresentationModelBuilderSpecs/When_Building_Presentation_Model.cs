using System;
using System.Collections.Generic;
using System.Linq;
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
            order = PurchaseOrder.MakeValidOrder();

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
        public void First_Part_Should_Be_OrderAttributes()
        {
            var part0 = model.Parts[0];
            part0.Title.ShouldEqual("Order Attributes");
            part0.Fields.Count.ShouldEqual(2);

            var field0 = part0.Fields[0];
            field0.Label.ShouldEqual("Ref.");
            //field0.Value.ShouldEqual("PO001");

            var field1 = part0.Fields[1];
            field1.Label.ShouldEqual("Order Date");
           // field1.Value.ShouldEqual(new DateTime(2010, 1, 12));

            var messageMap0 = part0.MessageMaps[0];
            messageMap0.Title.ShouldEqual("Change Order Attributes");
            messageMap0.Fields[0].ShouldEqual(part0.Fields[0]);
            messageMap0.Fields[1].ShouldEqual(part0.Fields[1]);
        }

        [It]
        public void First_Part_Should_Be_ChangeSupplier()
        {
            var part1 = model.Parts[1];
            part1.Title.ShouldEqual("Supplier");
//            part1.Fields.Count.ShouldEqual(1);

            var field0 = part1.Fields[0];
            field0.Label.ShouldEqual("Supplier Name");
           // field0.Value.ShouldEqual(new Supplier() { Name = "Elgar" });

            var messageMap0 = part1.MessageMaps[0];
            messageMap0.Title.ShouldEqual("Change Supplier And Update Supplier Prices");
            messageMap0.Fields[0].ShouldEqual(part1.Fields[0]);

            var messageMap1 = part1.MessageMaps[1];
            messageMap1.Title.ShouldEqual("Change Supplier");
//            messageMap1.Fields[0].ShouldEqual(part1.Fields[0]);
            
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