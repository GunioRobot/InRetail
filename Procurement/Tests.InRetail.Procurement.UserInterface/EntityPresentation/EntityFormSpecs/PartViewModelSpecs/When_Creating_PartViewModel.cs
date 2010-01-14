using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.PartViewModelSpecs
{
    public class When_Creating_PartViewModel : Specification
    {
        private IPartView view;
        private PartViewModel viewModel;

        public override void Given()
        {
            view = Moq.Mock<IPartView>();
        }

        public override void When()
        {
            viewModel = new PartViewModel(view);
        }

        [It]
        public void Should_Have_View_Property_Set()
        {
            viewModel.View.ShouldEqual(view);
        }
    }
    
}