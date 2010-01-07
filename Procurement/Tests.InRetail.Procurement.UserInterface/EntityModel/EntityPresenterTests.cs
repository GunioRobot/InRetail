using System;
using System.Collections.Generic;
using InRetail.UiCore.Actions;
using Moq;
using NServiceBus;

namespace Tests.InRetail.Procurement.UserInterface.EntityModel
{
    public class with_new_order : Specification
    {
        protected PurchaseOrderPresenter presenter;
        protected IPurchaseOrderView view;
        protected PurchaseOrder purchaseOrder;
        protected IEntityPartProvider entityPartProvider;
        protected IBus bus;

        public with_new_order()
        {
            bus = new Mock<IBus>().Object;
            entityPartProvider = new Mock<IEntityPartProvider>().Object;
            view = new Mock<IPurchaseOrderView>().Object;
            purchaseOrder = new PurchaseOrder();
            presenter = new PurchaseOrderPresenter(view, purchaseOrder, entityPartProvider, bus);
        }
    }

    public class When_creating_new_purchase_order_Gui : with_new_order
    {
        protected IPart constructionPart;

        public override void Given()
        {
            constructionPart = new Mock<IPart>().Object;
            constructionPart.Moq().SetupProperty(x => x.Confirmed, new Mock<IObservable<Unit>>().Object);
            entityPartProvider.Moq()
                .Setup(x => x.GetEntityConstructionPart()).Returns(constructionPart);
        }

        public override void When()
        {
            presenter.Activate(new Mock<IScreenObjectRegistry>().Object);
        }

        [It]
        public void Should_Show_Dialog_With_Entity_Construction_Part()
        {
            view.Moq().Verify(x => x.ShowDialog(constructionPart));
        }
    }

    public class When_user_confirms_new_purchase_order_creation_Gui : When_creating_new_purchase_order_Gui
    {
        protected IMessage retrievedMessage;
        protected Subject<Unit> confirmedSubject;

        public override void Given()
        {
            base.Given();
            retrievedMessage = new Mock<IMessage>().Object;
            constructionPart.Moq().Setup(x => x.GetMessage()).Returns(retrievedMessage);

            confirmedSubject = new Subject<Unit>();
            constructionPart.Moq().Setup(x => x.Confirmed).Returns(confirmedSubject);
        }

        public override void When()
        {
            presenter.Activate(new Mock<IScreenObjectRegistry>().Object);
            confirmedSubject.OnNext(new Unit());
        }

        [It]
        public void Should_Hide_Dialog()
        {
            view.Moq().Verify(x => x.CloseDialog());
        }

        [It]
        public void Should_Send_Message_to_bus()
        {
            bus.Moq().Verify(x => x.Send(retrievedMessage));
        }

    }
}