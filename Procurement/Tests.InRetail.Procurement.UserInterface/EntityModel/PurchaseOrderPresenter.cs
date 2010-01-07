using System;
using System.Collections.Generic;
using InRetail.Procurement.UserInterface.Views;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Extensions;
using InRetail.UiCore.Screens;
using NServiceBus;
using System.Linq;

namespace Tests.InRetail.Procurement.UserInterface.EntityModel
{
    public class PurchaseOrderPresenter : IScreen<PurchaseOrder>
    {
        private readonly IPurchaseOrderView _purchaseOrderView;
        private readonly PurchaseOrder _purchaseOrder;
        private readonly IEntityPartProvider _entityPartProvider;
        private readonly IBus _bus;
        private IDisposable _disposable;


        public PurchaseOrderPresenter(IPurchaseOrderView purchaseOrderView, PurchaseOrder purchaseOrder, IEntityPartProvider entityPartProvider, IBus bus)
        {
            _purchaseOrderView = purchaseOrderView;
            _purchaseOrder = purchaseOrder;
            _entityPartProvider = entityPartProvider;
            _bus = bus;
        }

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
            var entityConstructionPart = _entityPartProvider.GetEntityConstructionPart();
            _disposable = entityConstructionPart.Confirmed.Subscribe(_ => {
                                                                              _bus.Send(
                                                                                  entityConstructionPart.GetMessage());
                _purchaseOrderView.CloseDialog();
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
    }
}