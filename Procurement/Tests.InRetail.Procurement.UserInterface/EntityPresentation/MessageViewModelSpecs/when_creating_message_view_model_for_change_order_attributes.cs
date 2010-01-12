using System;
using InRetail.EntityPresentation;
using Tests.InRetail.Procurement.AssertHelpers;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_change_order_attributes : Specification
    {
        private MessageViewModel viewModel;
        private IMessageMap_v2 messageMap;

        public override void Given()
        {
            messageMap = Moq.Mock<IMessageMap_v2>();
            messageMap.SetupGet(x => x.Title).Returns("Change Order Attributes");
            var field1 = Moq.Mock<IField_v2>();
            field1.SetupGet(x => x.Label).Returns("Ref.");
            field1.SetupGet(x => x.Value).Returns("PO001");

            var field2 = Moq.Mock<IField_v2>();
            field2.SetupGet(x => x.Label).Returns("Order Date");
            field2.SetupGet(x => x.Value).Returns(new DateTime(2010, 1, 12));
            messageMap.Setup(x => x.Fields).Returns(new IField_v2[]
                                                        {
                                                            field1,
                                                            field2
                                                        });
        }

        public override void When()
        {
            viewModel = new MessageViewModel(messageMap,(f)=>new MessageValueFieldViewModel(f));
        }

        [It]
        public void Should_Have_Title()
        {
            viewModel.Title.ShouldEqual("Change Order Attributes");
        }

        [It]
        public void Should_Have_Fields_ViewModels()
        {
            viewModel.Fields[0].Label.ShouldEqual("Ref.");
            viewModel.Fields[0].OldValue.ShouldEqual("PO001");
            viewModel.Fields[0].NewValue.ShouldEqual("PO001");

            viewModel.Fields[1].Label.ShouldEqual("Order Date");
            viewModel.Fields[1].OldValue.ShouldEqual(new DateTime(2010, 1, 12));
            viewModel.Fields[1].NewValue.ShouldEqual(new DateTime(2010, 1, 12));
        }

        [It]
        public void Should_Disable_Send_Command()
        {
            viewModel.SendCommand.ShouldBeDisabled();
        }

        [It]
        public void Should_Enable_Cancel_Command()
        {
            viewModel.CancelCommand.ShouldBeEnabled();
        }
    }
}