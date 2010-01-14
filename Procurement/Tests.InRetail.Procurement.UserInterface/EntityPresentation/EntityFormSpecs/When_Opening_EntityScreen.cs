using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs
{
    public class When_Opening_EntityScreen : Specification
    {
        private EntityForm<PurchaseOrder> entityForm;
        private IEntityView view;
        private IPresentationModel presentationModel;

        public override void Given()
        {
            view = Moq.Mock<IEntityView>();
            
            presentationModel = Moq.Mock<IPresentationModel>();
            presentationModel.SetupGet(x => x.Title).Returns("aaaaa");
        }

        public override void When()
        {
            entityForm = new EntityForm<PurchaseOrder>(presentationModel, view);
        }

        [It]
        public void Should_Have_View_Property_Set()
        {
            entityForm.View.ShouldEqual(view);
        }

        [It]
        public void Should_Have_Title_Property_Set()
        {
            entityForm.Title.ShouldEqual("aaaaa");
        }

    }
}