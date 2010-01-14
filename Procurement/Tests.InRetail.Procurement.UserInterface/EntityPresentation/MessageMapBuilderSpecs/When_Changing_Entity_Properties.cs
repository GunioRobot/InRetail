using System;
using InRetail.UiCore.Extensions;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageMapBuilderSpecs
{
    public class When_Changing_Entity_Properties : When_Building_Message_Map
    {

        [It]
        public void Should_Changed_Mapped_Field_Value()
        {
            object newvalue1 = null;
            string newvalue2 = null;

            messageMap.Fields[0].ObservableValue.Subscribe(x => newvalue1 = x);
            messageMap.Fields[0].As<IField_v2<string>>().ObservableValue.Subscribe(x => newvalue2 = x);

            newvalue1.ShouldEqual(null);
            newvalue2.ShouldEqual(null);

            order.Ref = "Acho Magaria!";

            messageMap.Fields[0].Value.ShouldEqual("Acho Magaria!");
            newvalue1.ShouldEqual("Acho Magaria!");
            newvalue2.ShouldEqual("Acho Magaria!");

        }
    }
}