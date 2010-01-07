using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using InRetail.Procurement.Commands;
using InRetail.Procurement.UserInterface.Views;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;
using Microsoft.Practices.Composite.Presentation.Commands;
using NServiceBus;

namespace InRetail.Procurement.UserInterface.Purchasing
{
    public class PurchaseOrderScreen : IScreen<PurchaseOrder>
    {
        private readonly IPurchaseOrderView _view;
        private readonly PurchaseOrder _subject;
        private readonly IBus _bus;
        private IModelRepository<WhereHouse> _whereHouseRepo;
        private IModelRepository<Supplier> _supplierRepo;

        public PurchaseOrderScreen(IPurchaseOrderView view, PurchaseOrder subject, IBus bus)
        {
            _view = view;
            _subject = subject;
            _bus = bus;
            Submit = new DelegateCommand<object>(onSubmitExecute);
        }

        private void onSubmitExecute(object obj)
        {
            _subject.Id = Guid.NewGuid();

            _bus.Send<CreatePurchaseOrder>(po =>
                                               {
                                                   po.OrderId = _subject.Id;
                                                   po.SupplierId = _subject.Supplier.Id;
                                                   po.WhereHouseId = _subject.WhereHouse.Id;
                                               });

            Action<PurchaseOrderLine> sendMsg =
                l => _bus.Send<AddLineToPurchaseOrder>(b =>
                                                           {
                                                               b.PurchaseOrderId = _subject.Id;
                                                               b.LineId = l.Id;
                                                           });
            _subject.Lines.Run(sendMsg);
        }

        public void Dispose()
        {
        }

        public object View
        {
            get { return _view; }
        }

        public string Title
        {
            get { return "Purchase Order"; }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
        }

        public bool CanClose()
        {
            return true;
        }

        public PurchaseOrder Subject
        {
            get { return _subject; }
        }

        public IList<WhereHouse> WhereHouses { get; private set; }
        public IList<Supplier> Suppliers { get; private set; }
        public ICommand Submit { get; set; }
    }
}