using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.PresentationViewModel
{
    public class When_Creating_PresentationViewModel : Specification
    {
        private IPresentationView view;
        protected PresentationViewModel viewModel;

        public override void Given()
        {
            view = Moq.Mock<IPresentationView>();
        }

        public override void When()
        {
            viewModel = new PresentationViewModel(view);
        }

        [It]
        public void Should_Have_View_Property_Set()
        {
            viewModel.View.ShouldEqual(view);
        }
    }

    public class When_Adding_PartViewModel : When_Creating_PresentationViewModel
    {
        private IPartViewModel partVM;

        public override void Given()
        {
            partVM = Moq.Mock<IPartViewModel>();
        }

        public override void When()
        {
            base.Given();
            //viewModel.Add(partVM);
        }

        [It]
        public void Should_Call_View_To_Bind_Part()
        {
            
        }
    }

    public interface IPartViewModel {}
}