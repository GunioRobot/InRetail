using System;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_Add_OrderLine : With_New_Context
    {

        public override void Given()
        {
            base.Given();
            messageMap.Setup(x => x.Title).Returns("Add OrderLine");
        }

        public override void When()
        {
            base.When();
        }

        [It]
        public void Should_Have_Title()
        {
            viewModel.Title.ShouldEqual("Add OrderLine");
        }
    }
}