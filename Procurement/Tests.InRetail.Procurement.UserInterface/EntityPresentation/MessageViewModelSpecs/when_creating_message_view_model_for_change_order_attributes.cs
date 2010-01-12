using System;
using InRetail.EntityPresentation;
using InRetail.UiCore.Extensions;
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
            var field0 = Moq.Mock<IField_v2<string>>();
            field0.SetupGet(x => x.Label).Returns("Ref.");
            field0.SetupGet(x => x.Value).Returns("PO001");
            field0.Setup(x => x.BuildViewModel()).Returns(new ValueFieldViewModel<string>(field0));

            var field1 = Moq.Mock<IField_v2<DateTime>>();
            field1.SetupGet(x => x.Label).Returns("Order Date");
            field1.SetupGet(x => x.Value).Returns(new DateTime(2010, 1, 12));
            field1.Setup(x => x.BuildViewModel()).Returns(new ValueFieldViewModel<DateTime>(field1));

            messageMap.Setup(x => x.Fields).Returns(new IField_v2[]
                                                        {
                                                            field0,
                                                            field1
                                                        });
        }

        public override void When()
        {
            viewModel = new MessageViewModel(messageMap);
        }

        [It]
        public void Should_Have_Title()
        {
            viewModel.Title.ShouldEqual("Change Order Attributes");
        }

        [It]
        public void Should_Have_Fields_ViewModels()
        {
            var fieldViewModel0 = viewModel.Fields[0].As<ValueFieldViewModel<String>>();
            fieldViewModel0.Label.ShouldEqual("Ref.");
            fieldViewModel0.OldValue.ShouldEqual("PO001");
            fieldViewModel0.NewValue.ShouldEqual("PO001");

            var fieldViewModel1 = viewModel.Fields[1].As<ValueFieldViewModel<DateTime>>();
            fieldViewModel1.Label.ShouldEqual("Order Date");
            fieldViewModel1.OldValue.ShouldEqual(new DateTime(2010, 1, 12));
            fieldViewModel1.NewValue.ShouldEqual(new DateTime(2010, 1, 12));
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