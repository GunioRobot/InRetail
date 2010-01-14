using System;
using System.Linq;
using InRetail.EntityPresentation;
using InRetail.UiCore.Extensions;
using Tests.InRetail.Procurement.AssertHelpers;
using Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs;
using Tests.InRetail.Procurement.EntityPresentation.EntityPresentationModelBuilderSpecs;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_change_order_attributes : With_New_Context
    {
        private IFieldViewModelLocator fieldViewModelLocator;
        public override void Given()
        {
            base.Given();
            
            
            
           messageMap = New.MessageMap_v2("Change Order Attributes")
                .Fields(x => x.Label("Ref.").Value("PO001"),
                        x => x.Label("Order Date").Value(new DateTime(2010, 1, 12))).Build();
           var fieldV2s = messageMap.Fields.ToList();

            fieldViewModelLocator = Moq.Mock<IFieldViewModelLocator>();
            fieldViewModelLocator.Setup(x => x.GetViewModel(fieldV2s[0]))
                .Returns((IField_v2 x) =>new ValueFieldViewModel<string>(x));
            fieldViewModelLocator.Setup(x => x.GetViewModel(fieldV2s[1]))
                .Returns((IField_v2 x) => new ValueFieldViewModel<DateTime>(x));
        }

        public override void When()
        {
            base.When();
            viewModel = new MessageViewModel(messageMap, fieldViewModelLocator);
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