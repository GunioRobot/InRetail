using InRetail.UiCore.Dialogs;
using Microsoft.Practices.Composite.Presentation.Commands;
using NServiceBus;
using ProductCatalogContracts;

namespace InRetail.ProductCatalog.Commands.CreateProduct
{
    public class CreateProductCommandDialog : ICommandDialog<CreateProductMessage>
    {
        private readonly IBus _bus;
        private readonly CreateProductMessage _message;

        public CreateProductCommandDialog(CreateProductMessage message, IBus bus)
        {
            _message = message;
            _bus = bus;
            SendCommand = new DelegateCommand<object>(_ => _bus.Send(_message));
        }

        public CreateProductMessage Message
        {
            get { return _message; }
        }

        public DelegateCommand<object> SendCommand { get; set; }

        public string Title
        {
            get { return "Create Product"; }
        }
    }
}