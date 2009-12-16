using InRetail.UiCore.Dialogs;

namespace InRetail.ProductCatalog.Commands.CreateProduct
{
    public class CreateProductCommandDialog : ICommandDialog<CreateProductCommand>
    {
        private readonly CreateProductCommand _command;

        public CreateProductCommandDialog()
        {
            
        }
        public CreateProductCommandDialog(CreateProductCommand command):this()
        {
            _command = command;
        }

        public CreateProductCommand Command
        {
            get { return _command; }
        }

        public string Title
        {
            get { return "Create Product"; }
        }
    }
}