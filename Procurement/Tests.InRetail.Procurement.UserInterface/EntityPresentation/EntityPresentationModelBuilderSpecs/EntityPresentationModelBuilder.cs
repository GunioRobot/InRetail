using System;
using System.Collections.Generic;
using InRetail.EntityPresentation;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    internal class EntityPresentationModelBuilder
    {
        public PresentationModel Build(IEntity order)
        {
            var presentationModel = new PresentationModel()
                                        {
                                            Title = order.GetEntityScreenName()
                                        };
            var part = new Part() { Title = "Order Attributes" };
            part.AddField(new Field() { Label = "Ref.", Value = "PO001" });
            part.AddField(new Field() { Label = "Order Date", Value = new DateTime(2010, 1, 12) });
            presentationModel.AddPart(part);
            return presentationModel;
        }
    }
}