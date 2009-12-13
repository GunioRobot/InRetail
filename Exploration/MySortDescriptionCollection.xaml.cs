using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using InRetail.UiCore.Extensions;

namespace Exploration
{
    public class MySortDescriptionCollection : SortDescriptionCollection,IObservable<NotificationMessage>
    {
        private IList<IObserver<NotificationMessage>> _messages=new List<IObserver<NotificationMessage>>();

        protected override void ClearItems()
        {
            NotifyObservers(new NotificationMessage() { Message = this + string.Format(".ClearItems()") });
            base.ClearItems();
        }

        protected override void InsertItem(int index, SortDescription item)
        {
            NotifyObservers(new NotificationMessage() { Message = this + string.Format(".InsertItem({0}, {1})", index, item)});
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            NotifyObservers(new NotificationMessage() { Message = this + string.Format(".RemoveItem({0})", index) });
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, SortDescription item)
        {
            NotifyObservers(new NotificationMessage() { Message = this + string.Format(".SetItem({0}, {1})", index, item) });
            base.SetItem(index, item);
        }
        private void NotifyObservers(NotificationMessage message)
        {
            _messages.Each(x=>x.OnNext(message));
        }
        public IDisposable Subscribe(IObserver<NotificationMessage> observer)
        {
            _messages.Add(observer);
            return new DisposableSubscription(() => _messages.Remove(observer));
        }
        
    }
}