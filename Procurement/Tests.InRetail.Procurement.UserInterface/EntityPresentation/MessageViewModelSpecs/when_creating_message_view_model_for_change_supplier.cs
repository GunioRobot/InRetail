using System;
using System.Collections.Generic;
using InRetail.UiCore.Extensions;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class when_creating_message_view_model_for_change_supplier : With_New_Context
    {
        
        private ILookUpDataProvider lookUpDataProvider;

        public override void Given()
        {
            base.Given();
            lookUpDataProvider = Moq.Mock<ILookUpDataProvider>();
            lookUpDataProvider.Setup(x => x.GetLookupData<Supplier>()).Returns(new[]
                                                                        {
                                                                            new Supplier() {Name = "Elgar"},
                                                                            new Supplier() {Name = "Supplier2"},
                                                                            new Supplier() { Name = "Supplier3" }
                                                                        });
            
            messageMap.SetupGet(x => x.Title).Returns("Supplier");

            var field0 = Moq.Mock<IField_v2<Supplier>>();
            field0.SetupGet(x => x.Label).Returns("Supplier Name");
            field0.SetupGet(x => x.Value).Returns(new Supplier() { Name = "Elgar" });
            //field0.Setup(x => x.BuildViewModel()).Returns(new ReferenceFieldViewModel<Supplier>(field0, lookUpDataProvider));

            messageMap.Setup(x => x.Fields).Returns(new[] { field0, });

          
        }

        public override void When()
        {
            base.When();
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
}