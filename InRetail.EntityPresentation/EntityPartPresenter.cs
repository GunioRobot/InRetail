using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace InRetail.EntityPresentation
{
    public interface IFieldView { }
    public class EntityPartPresenter : IPartPresenter
    {
        private readonly IPart _part;
        private readonly IEntityPartView _partView;
        private readonly ICommand _sendCommand;
        private readonly ICommand _cancelCommand;
        IList<MessageCommandViewModel> _messageCommands;
        private bool InEditingMode = false;
        public IList<IFieldView> _fieldViews;
        public IList<IFieldView> FieldViews
        {
            get { return _fieldViews; }
        }

        public EntityPartPresenter()
        {
        }

        public EntityPartPresenter(IPart part, IEntityPartView partView)
        {
            _part = part;
            _partView = partView;
            _fieldViews= new ObservableCollection<IFieldView>();
            part.Fields.Run(x => _fieldViews.Add(x.BuildFieldView()));

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
            get {
                return _messageCommands ??
                       (_messageCommands = new List<MessageCommandViewModel>(
                           _part.MessageMaps.Select(x => buildEditMessageCommand(x))));
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