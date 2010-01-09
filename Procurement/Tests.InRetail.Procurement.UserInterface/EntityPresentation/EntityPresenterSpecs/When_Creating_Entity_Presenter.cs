using Moq;
using Tests.InRetail.Procurement.AssertHelpers;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityPresenterSpecs
{
    public class When_Creating_Entity_Presenter : Specification
    {
        private EntityPresenter<PurchaseOrder> entityPresenter;
        private EntityPartPresenter[] _entityPartsPresenterPresenter;
        private PurchaseOrder purchaseOrder;
        private IEntityPartProvider<PurchaseOrder> partProvider;

        public override void Given()
        {
            partProvider = new Mock<IEntityPartProvider<PurchaseOrder>>().Object;
            purchaseOrder = new PurchaseOrder();
            

            _entityPartsPresenterPresenter = new[] { new Mock<EntityPartPresenter>().Object };
            partProvider.Setup(x => x.GetEntityParts()).Returns(_entityPartsPresenterPresenter);
        }

        public override void When()
        {
            entityPresenter = new EntityPresenter<PurchaseOrder>(partProvider, purchaseOrder);
        }

        [It]
        public void Should_Have_EntityParts_Populated()
        {
            entityPresenter.EntityParts.ShouldContainEqualElements(_entityPartsPresenterPresenter);
        }

        [It]
        public void TitleProperty_Should_Have_Value_Provided_By_Entity()
        {
            entityPresenter.Title.ShouldEqual("PurchaseOrder");
        }
    }
}