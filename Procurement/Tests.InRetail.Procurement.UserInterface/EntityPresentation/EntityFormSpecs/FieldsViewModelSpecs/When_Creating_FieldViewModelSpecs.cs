using System;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.FieldsViewModelSpecs
{
    public class When_Creating_FieldViewModelSpecs : Specification
    {
        protected FieldViewModelBase viewModel;
        private IFieldView view;
        protected IField_v2 field;

        public override void Given()
        {
            view = Moq.Stub<IFieldView>();
            field = Moq.Stub<IField_v2>();
            field.SetupGet(x => x.Label).Returns("aaaa");
            field.SetupGet(x => x.Value).Returns("bbbb");
            field.SetupGet(x => x.ObservableValue).Returns(Moq.Stub<IObservable<object>>());
        }

        public override void When()
        {
            viewModel = new TestableFieldViewModelBase(view, field);
        }

        [It]
        public void Should_Have_View_Property_Set()
        {
            viewModel.View.ShouldEqual(view);
        }

        [It]
        public void Should_Have_Label_Property_Set()
        {
            viewModel.Label.ShouldEqual("aaaa");
        }

        [It]
        public void Should_Have_Value_Property_Set()
        {
            viewModel.Value.ShouldEqual("bbbb");
        }
    }
}