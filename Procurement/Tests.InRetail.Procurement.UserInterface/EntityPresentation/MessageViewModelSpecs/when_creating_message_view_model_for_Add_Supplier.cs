using System;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_Add_OrderLine : With_New_Context
    {
        private IFieldViewModelLocator fieldViewModelLocator;
        public override void Given()
        {
            base.Given();
            messageMap.Setup(x => x.Title).Returns("Add OrderLine");
        }

        public override void When()
        {
            base.When();
            viewModel = new MessageViewModel(messageMap, fieldViewModelLocator);
        }

        [It]
        public void Should_Have_Title()
        {
            viewModel.Title.ShouldEqual("Add OrderLine");
        }
    }
}