using System;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Extensions;
using InRetail.UiCore.Screens;
using NServiceBus;
using Tests.InRetail.Procurement.EntityPresentation;

namespace Tests.InRetail.Procurement.Scenarios.Working_With_Purchase_Order.Fakes
{
    public class PurchaseOrderPresenter : IScreen<PurchaseOrder>
    {
        private readonly IBus _bus;
        private readonly IEntityPartProvider<PurchaseOrder> _entityPartProvider;
        private readonly PurchaseOrder _purchaseOrder;
        private readonly IPurchaseOrderView _purchaseOrderView;
        private IDisposable _disposable;


        public PurchaseOrderPresenter(IPurchaseOrderView purchaseOrderView, PurchaseOrder purchaseOrder,
                                      IEntityPartProvider<PurchaseOrder> entityPartProvider, IBus bus)
        {
            _purchaseOrderView = purchaseOrderView;
            _purchaseOrder = purchaseOrder;
            _entityPartProvider = entityPartProvider;
            _bus = bus;
        }

        #region IScreen<PurchaseOrder> Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object View
        {
            get { return _purchaseOrderView; }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
            IPart entityConstructionPart = _entityPartProvider.GetEntityConstructionPart();
            _disposable = entityConstructionPart.Confirmed.Subscribe(_ =>
                                                                         {
                                                                             _bus.Send(
                                                                                 entityConstructionPart.GetMessage());
                                                                             _purchaseOrderView.CloseDialog();
                                                                             _purchaseOrderView.Busy();

                                                                         });
            _purchaseOrderView.As<IPurchaseOrderView>().ShowDialog(entityConstructionPart);
        }

        public bool CanClose()
        {
            throw new NotImplementedException();
        }


        public PurchaseOrder Subject
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}