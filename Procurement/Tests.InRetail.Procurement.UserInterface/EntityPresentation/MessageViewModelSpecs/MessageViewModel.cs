using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Tests.InRetail.Procurement.EntityPresentation.MessageViewModelSpecs
{
    public class MessageViewModel
    {
        

        public MessageViewModel(IMessageMap_v2 messageMap, Func<IField_v2, MessageValueFieldViewModelBase> viewModelFactory)
        {
        
            Title = messageMap.Title;

            Fields = new List<MessageValueFieldViewModelBase>();

            messageMap.Fields.Run(x => Fields.Add(viewModelFactory(x)));

            SendCommand = new DelegateCommand<object>(x => { }, x => false);
            CancelCommand = new DelegateCommand<object>(x => { }, x => true);
        }

        public string Title { get; set; }
        public IList<MessageValueFieldViewModelBase> Fields { get; private set; }
        public ICommand SendCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}