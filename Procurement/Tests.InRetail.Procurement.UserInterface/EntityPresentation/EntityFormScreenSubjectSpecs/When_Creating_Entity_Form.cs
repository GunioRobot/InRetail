using System;
using InRetail.EntityPresentation;
using InRetail.UiCore.Extensions;
using InRetail.UiCore.Screens;
using Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormScreenSubjectSpecs
{
    public class When_Creating_EntityFormScreenSubject : Specification
    {
        protected EntityFormScreenSubject<PurchaseOrder> formSubject;
        protected PurchaseOrder purchaseOrder;

        public override void Given()
        {
            purchaseOrder = new PurchaseOrder();
        }

        public override void When()
        {
            formSubject = new EntityFormScreenSubject<PurchaseOrder>(purchaseOrder);
        }
    }

    public class When_Creating_EntityForm : When_Creating_EntityFormScreenSubject
    {
        private IScreenFactory screenFactory;
        public override void Given()
        {
            base.Given();
            screenFactory = Moq.Mock<IScreenFactory>();
        }

        public override void When()
        {
            base.When();
            formSubject.CreateScreen(screenFactory);
        }

        [It]
        public void Should_Call_ScreenFactory_With_Subject()
        {
            screenFactory.Verify(x => x.Build<PurchaseOrder>(purchaseOrder));
        }
    }




    public class EntityFormScreenSubject<T> : IScreenSubject<T> where T : IEntity
    {
        private readonly T _purchaseOrder;

        public EntityFormScreenSubject(T purchaseOrder)
        {
            _purchaseOrder = purchaseOrder;

        }

        public bool Matches(IScreen screen)
        {
            if (screen is IEntityForm<T> && screen.As<IEntityForm<T>>().Subject.Equals(_purchaseOrder))
                return true;
            return false;
        }

        public IScreen CreateScreen(IScreenFactory factory)
        {
            return factory.Build(_purchaseOrder);
        }
    }
}