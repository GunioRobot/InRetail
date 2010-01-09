using System;
using System.Collections.Generic;
using InRetail.UiCore.Actions;
using Moq;
using NServiceBus;
using Tests.InRetail.Procurement.EntityPresentation;
using Tests.InRetail.Procurement.Scenarios.Working_With_Purchase_Order.Fakes;

namespace Tests.InRetail.Procurement.Scenarios.Working_With_Purchase_Order
{
    public class with_new_order : Specification
    {
        protected IBus bus;
        protected IEntityPartProvider<PurchaseOrder> entityPartProvider;
        protected PurchaseOrderPresenter presenter;
        protected PurchaseOrder purchaseOrder;
        protected IPurchaseOrderView view;

        public with_new_order()
        {
            bus = new Mock<IBus>().Object;
            entityPartProvider = new Mock<IEntityPartProvider<PurchaseOrder>>().Object;
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
            entityPartProvider.Moq().Setup(x => x.GetEntityConstructionPart()).Returns(constructionPart);
        }

        public override void When()
        {
            presenter.Activate(new Mock<IScreenObjectRegistry>().Object);
        }

        [It]
        public void Should_Show_Dialog_With_Entity_Construction_Part()
        {
            view.Verify(x => x.ShowDialog(constructionPart));
        }
    }

    public class When_user_confirms_new_purchase_order_creation_Gui : When_creating_new_purchase_order_Gui
    {
        protected Subject<Unit> confirmedSubject;
        protected IMessage retrievedMessage;

        public override void Given()
        {
            base.Given();

            retrievedMessage = new Mock<IMessage>().Object;
            constructionPart.Setup(x => x.GetMessage()).Returns(retrievedMessage);

            confirmedSubject = new Subject<Unit>();
            constructionPart.Setup(x => x.Confirmed).Returns(confirmedSubject);
        }

        public override void When()
        {
            base.When();
            confirmedSubject.OnNext(new Unit());
        }

        [It]
        public void Should_Hide_Dialog()
        {
            view.Verify(x => x.CloseDialog());
        }

        [It]
        public void Should_Send_Message_To_Bus()
        {
            bus.Verify(x => x.Send(retrievedMessage));
        }
      
        [It]
        public void Should_Change_View_State_To_Busy()
        {
            view.Verify(x => x.Busy());
        }
    }
}