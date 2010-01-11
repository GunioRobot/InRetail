using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace InRetail.EntityPresentation
{
    public class EntityPartPresenter : IPartPresenter
    {
        private readonly IPart _part;
        private readonly IEntityPartView _partView;
        private readonly ICommand _sendCommand;
        private readonly ICommand _cancelCommand;
        IList<MessageCommandViewModel> _messageCommands;
        private bool InEditingMode = false;


        public EntityPartPresenter()
        {
        }

        public EntityPartPresenter(IPart part, IEntityPartView partView)
        {
            _part = part;
            _partView = partView;


            _sendCommand = new DelegateCommand<object>(x => { _partView.SwitchToViewMode();
                                                                InEditingMode = false; }, x => InEditingMode);
            _cancelCommand = new DelegateCommand<object>(x => { _partView.SwitchToViewMode();
                                                                  InEditingMode = false; }, x => InEditingMode);

        }

        public ICommand SendCommand
        {
            get { return _sendCommand; }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }
        public IList<MessageCommandViewModel> MessageCommands
        {
            get
            {
                if (_messageCommands == null)
                    _messageCommands = 
                        new List<MessageCommandViewModel>(_part.MessageMaps.Select(x => buildEditMessageCommand(x)));
                return _messageCommands;
            }
        }

        private MessageCommandViewModel buildEditMessageCommand(IMessageMap x)
        {
            return new MessageCommandViewModel()
                       {
                           Name = x.Name,
                           Command = new DelegateCommand<object>(p =>
                                                                     {
                                                                         _partView.SwitchToEditMode(x.BuildMessageView());
                                                                         InEditingMode = true;
                                                                     })
                       };
        }

        public bool CanClose()
        {
            throw new NotImplementedException();
        }


    }
}