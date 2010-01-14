using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;
using Xunit.Extensions;
using System.Windows;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    public class When_Building_Message_Map : Specification
    {
        protected MessageMap messageMap;
        private ChangeOrderAttributesMap map;
        protected PurchaseOrder order;

        public override void Given()
        {
            order = PurchaseOrder.MakeValidOrder();

            map = new ChangeOrderAttributesMap();
        }

        public override void When()
        {
            messageMap = map.Build(order);
        }

        [It]
        public void Mapped_Fields_Should_Have_Entity_Data()
        {
            messageMap.Fields[0].Label.ShouldEqual("Ref");
            messageMap.Fields[0].Value.ShouldEqual("PO001");

            messageMap.Fields[1].Label.ShouldEqual("Date");
            messageMap.Fields[1].Value.ShouldEqual(new DateTime(2010, 1, 12));
        }
    }
}