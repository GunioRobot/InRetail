using System.Collections.Generic;
using Moq;
using InRetail.EntityPresentation;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityScreenSpecs
{
    public class When_Opening_EntityScreen : Specification
    {
        private EntityScreen<PurchaseOrder> screen;
        private IModelBuilder modelBuilder;
        private PurchaseOrder entity;
        private IModelViewModelLocator viewModelLocator;
        private IEntityView view;
        private IPresentationView presentationView;
        private IPresentationModel presentationModel;

        public override void Given()
        {
            view = Moq.Mock<IEntityView>();
            entity = new PurchaseOrder();

            modelBuilder = Moq.Mock<IModelBuilder>();
            presentationModel = Moq.Mock<IPresentationModel>();
            presentationModel.SetupGet(x=>x.Title).Returns("aaaaa");
            modelBuilder.Setup(x => x.Build(entity)).Returns(presentationModel);

            var presentationViewModel = Moq.Mock<IPresentationViewModel>();
            presentationView = Moq.Mock<IPresentationView>();
            presentationViewModel.SetupGet(x => x.View).Returns(presentationView);

            viewModelLocator = Moq.Mock<IModelViewModelLocator>();
            viewModelLocator.Setup(x => x.BuildViewModel(presentationModel)).Returns(presentationViewModel);
        }

        public override void When()
        {
            screen = new EntityScreen<PurchaseOrder>(entity, modelBuilder, viewModelLocator, view);
        }

        [It]
        public void Should_Have_View_Property_Set()
        {
            screen.View.ShouldEqual(view);
        }

        [It]
        public void Should_Have_Title_Property_Set()
        {
            screen.Title.ShouldEqual("aaaaa");
        }

        [It]
        public void Should_Call_ModelBuilder_To_Build_Model()
        {
            modelBuilder.Verify(x => x.Build<PurchaseOrder>(entity));
        }

        [It]
        public void Should_Locate_PresentationViewModel()
        {
            viewModelLocator.Verify(x => x.BuildViewModel(presentationModel));
        }

        [It]
        public void Should_Call_View_To_Show_PresentationModels_ViewModel_View()
        {
            view.Verify(x => x.ShowModelView(presentationView));
        }

    }
}