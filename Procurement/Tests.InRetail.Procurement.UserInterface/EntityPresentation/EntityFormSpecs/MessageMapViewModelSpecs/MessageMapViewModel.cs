using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs;

namespace Tests.InRetail.Procurement.EntityPresentation.EntityFormSpecs.MessageMapViewModelSpecs
{
    public class MessageMapViewModel
    {
        private readonly ICommand _commands;
        private readonly IMessageSender _messageSender;
        private readonly IMessageMap_v2 _model;

        public MessageMapViewModel(IMessageSender messageSender, IMessageMap_v2 model)
        {
            _messageSender = messageSender;
            _model = model;
            _commands = new DelegateCommand<object>(sendExecute);
        }

        public ICommand SendCommand { get { return _commands; } }

        private void sendExecute(object obj) { _messageSender.SendMessage(_model.MessageType); }
    }
}