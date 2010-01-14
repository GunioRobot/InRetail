using System;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.FieldsViewModelSpecs
{
    public class When_Source_Field_Value_Changes : When_Creating_FieldViewModelSpecs
    {
        private Action when;

        public override void Given()
        {
            base.Given();
            when = field.ChangesValue("gggg"); 
        }

        public override void When()
        {
            base.When();
        }

        [It]
        public void Should_Fire_Property_Changed_Event()
        {
            viewModel.FromPropertyChanged(x => x.Value).Subscribe(x => x.ShouldEqual("gggg"));
            viewModel.Value.ShouldEqual("bbbb"); 
            when();
        }

        [It]
        public void Should_Have_New_Value_Property_Set()
        {
            viewModel.Value.ShouldEqual("bbbb");
            when();
            viewModel.Value.ShouldEqual("gggg");
        }
    }
}