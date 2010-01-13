using System;
using System.Collections.Generic;
using InRetail.EntityPresentation;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs
{
    internal class EntityPresentationModelBuilder
    {
        public PresentationModel Build(IEntity order)
        {
            var presentationModel = new PresentationModel(){Title = order.GetEntityScreenName()};

            var part0 = GetPart("Order Attributes");
                var makeMessage0 = MakeMessage("Change Order Attributes");
                    makeMessage0.AddField(GetField("Ref.", "PO001"));
                    makeMessage0.AddField(GetField("Order Date", new DateTime(2010, 1, 12)));
                part0.AddMessageMap(makeMessage0);

            var part1 = GetPart("Supplier");
                var makeMessage1 = MakeMessage("Change Supplier And Update Supplier Prices");
                    makeMessage1.AddField(GetField("Supplier Name", new Supplier() { Name = "Elgar" }));
                part1.AddMessageMap(makeMessage1);
            
                var makeMessage2 = MakeMessage("Change Supplier");
                    makeMessage2.AddField(GetField("Supplier Name", new Supplier() { Name = "Elgar" }));
                part1.AddMessageMap(makeMessage2);

            presentationModel.AddPart(part0);
            presentationModel.AddPart(part1);
            return presentationModel;
        }

        private Part GetPart(string partTitle) 
        {
            return new Part() { Title = partTitle }; ;
        }

        private MessageMap MakeMessage(string title)
        {
            return new MessageMap(title);
        }

        private Field GetField(string label, object value)
        {
            return new Field() { Label = label, Value = value };
        }
    }
}