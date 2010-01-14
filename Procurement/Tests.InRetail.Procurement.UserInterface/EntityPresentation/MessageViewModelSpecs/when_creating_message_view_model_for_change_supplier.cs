using System;
using System.Collections.Generic;
using System.Linq;
using InRetail.UiCore.Extensions;
using Tests.InRetail.Procurement.EntityPresentation.EntityPartPresenterSpecs;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_change_supplier : With_New_Context
    {
        private IFieldViewModelLocator fieldViewModelLocator;

        public override void Given()
        {
            base.Given();

            messageMap = New.MessageMap_v2("Supplier")
                .Fields(x => x.Label("Supplier Name")
                                .Value(new Supplier() { Name = "Elgar" })
                            ).Build();
            var fieldV2s = messageMap.Fields.ToList();

            var lookUpDataProvider = Moq.Mock<ILookUpDataProvider>();
            lookUpDataProvider.Setup(x => x.GetLookupData<Supplier>())
                .Returns(new[]
                             {
                                 new Supplier() {Name = "Elgar"},
                                 new Supplier() {Name = "Supplier2"},
                                 new Supplier() {Name = "Supplier3"}
                             });

            fieldViewModelLocator = Moq.Mock<IFieldViewModelLocator>();
            fieldViewModelLocator.Setup(x => x.GetViewModel(fieldV2s[0]))
                .Returns((IField_v2 x) => new ReferenceFieldViewModel<Supplier>(x, lookUpDataProvider));
        }

        public override void When()
        {
            base.When();
            viewModel = new MessageViewModel(messageMap, fieldViewModelLocator);
        }

        [It]
        public void Should_Have_Title()
        {
            viewModel.Title.ShouldEqual("Supplier");
        }

        [It]
        public void Should_Have_Fields_ViewModels()
        {
            var fieldViewModel0 = viewModel.Fields[0].As<ReferenceFieldViewModel<Supplier>>();
            fieldViewModel0.Label = "Supplier Name";
            fieldViewModel0.OldValue = new Supplier() { Name = "Elgar" };
            fieldViewModel0.NewValue = new Supplier() { Name = "Elgar" };

            fieldViewModel0.LookUpData.ShouldContain(new Supplier() { Name = "Elgar" });
            fieldViewModel0.LookUpData.ShouldContain(new Supplier() { Name = "Supplier2" });
            fieldViewModel0.LookUpData.ShouldContain(new Supplier() { Name = "Supplier3" });
        }
    }

    public interface IFieldViewModelLocator
    {
        IFieldViewModel GetViewModel(IField_v2 field0);
    }
}