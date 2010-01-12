using System;
using System.Collections.Generic;
using InRetail.UiCore.Extensions;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_change_supplier : Specification
    {
        private IMessageMap_v2 messageMap;
        private MessageViewModel viewModel;
        private ILookUpDataProvider lookUpDataProvider;

        public override void Given()
        {
            messageMap = Moq.Mock<IMessageMap_v2>();
            messageMap.SetupGet(x => x.Title).Returns("Supplier");

            var field1 = Moq.Mock<IField_v2>();
            field1.SetupGet(x => x.Label).Returns("Supplier Name");
            field1.SetupGet(x => x.Value).Returns(new Supplier() { Name = "Elgar" });

            messageMap.Setup(x => x.Fields).Returns(new[] { field1, });

            lookUpDataProvider = Moq.Mock<ILookUpDataProvider>();
            lookUpDataProvider.Setup(x => x.GetSuppliers()).Returns(new[]
                                                                        {
                                                                            new Supplier() {Name = "Elgar"},
                                                                            new Supplier() {Name = "Supplier2"},
                                                                            new Supplier() { Name = "Supplier3" }
                                                                        });
        }

        public override void When()
        {
            viewModel = new MessageViewModel(messageMap, (x) => new MessageRefFieldViewModel(x, lookUpDataProvider));
        }

        [It]
        public void Should_Have_Title()
        {
            viewModel.Title.ShouldEqual("Supplier");
        }

        [It]
        public void Should_Have_Fields_ViewModels()
        {
            viewModel.Fields[0].Label = "Supplier Name";
            viewModel.Fields[0].OldValue = new Supplier() { Name = "Elgar" };
            viewModel.Fields[0].NewValue = new Supplier() { Name = "Elgar" };

            var refFieldViewModel = viewModel.Fields[0].As<MessageRefFieldViewModel>();
            refFieldViewModel.LookUpData.ShouldContain(new Supplier() { Name = "Elgar" });
            refFieldViewModel.LookUpData.ShouldContain(new Supplier() { Name = "Supplier2" });
            refFieldViewModel.LookUpData.ShouldContain(new Supplier() { Name = "Supplier3" });
        }
    }
}