using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;

namespace Exploration
{
    public class MyCollectionView : ListCollectionView, IObservable<NotificationMessage>
    {
        private readonly List<IObserver<NotificationMessage>> _observers = new List<IObserver<NotificationMessage>>();
        private MySortDescriptionCollection _collection;

        public MyCollectionView(IList list)
            : base(list)
        {
        }


        public override bool CanGroup
        {
            get
            {
                NotifyObservers(new NotificationMessage
                                    {

                                        Message = this + string.Format(".Get CanGroup")
                                    });
                return base.CanGroup;
            }
        }

        public override bool CanSort
        {
            get
            {
                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get CanSort") });
                return base.CanSort;
            }
        }

        public override bool CanFilter
        {
            get
            {
                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get CanFilter") });
                return base.CanFilter;
            }
        }

        public override Predicate<object> Filter
        {
            get
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get Filter") });
                return base.Filter;
            }
            set
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Set Filter = {0}", value) });
                base.Filter = value;
            }
        }

        public override GroupDescriptionSelectorCallback GroupBySelector
        {
            get
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get GroupBySelector") });
                return base.GroupBySelector;
            }
            set
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Set GroupBySelector = {0}", value) });
                base.GroupBySelector = value;
            }
        }

        public override int Count
        {
            get
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get Count") });
                return base.Count;
            }
        }

        public override bool IsEmpty
        {
            get
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get IsEmpty") });
                return base.IsEmpty;
            }
        }

        public override SortDescriptionCollection SortDescriptions
        {
            get
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get SortDescriptions") });
                if (_collection == null){
                    _collection = new MySortDescriptionCollection();
                    _collection.Subscribe(NotifyObservers);
                }
                return _collection;  // base.SortDescriptions;
            }
        }

        public override ReadOnlyObservableCollection<object> Groups
        {
            get
            {
                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get Groups") });
                return base.Groups;
            }
        }

        public override ObservableCollection<GroupDescription> GroupDescriptions
        {
            get
            {

                NotifyObservers(new NotificationMessage { Message = this + string.Format(".Get GroupDescriptions") });
                return base.GroupDescriptions;
            }
        }

        public IDisposable Subscribe(IObserver<NotificationMessage> observer)
        {
            _observers.Add(observer);
            return new DisposableSubscription(() => _observers.Remove(observer));
        }

        protected override int Compare(object o1, object o2)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".Compare({0}, {1})", o1, o2) });
            return base.Compare(o1, o2);
        }

        public override bool Contains(object item)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".Contains({0})", item) });
            return base.Contains(item);
        }

        protected override IEnumerator GetEnumerator()
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".GetEnumerator()") });
            return base.GetEnumerator();
        }

        public override object GetItemAt(int index)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".GetItemAt({0})", index) });
            return base.GetItemAt(index);
        }

        public override int IndexOf(object item)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".IndexOf({0})", item) });

            return base.IndexOf(item);
        }

        public override bool MoveCurrentToPosition(int position)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".MoveCurrentToPosition({0})", position) });
            return base.MoveCurrentToPosition(position);
        }

        private void NotifyObservers(NotificationMessage message)
        {
            if (message != null) _observers.ForEach(o => o.OnNext(message));
        }

        protected override void OnBeginChangeLogging(NotifyCollectionChangedEventArgs args)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".OnBeginChangeLogging({0})", args) });
            base.OnBeginChangeLogging(args);
            
        }

        public override bool PassesFilter(object item)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".PassesFilter({0})", item) });
            return base.PassesFilter(item);
        }

        protected override void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
        {

            NotifyObservers(new NotificationMessage { Message = this + string.Format(".ProcessCollectionChanged({0})", args) });
            base.ProcessCollectionChanged(args);
        }

        protected override void RefreshOverride()
        {
            NotifyObservers(new NotificationMessage { Message = this + string.Format(".RefreshOverride()") });
            base.RefreshOverride();
        }

       
    }

    internal class DisposableSubscription : IDisposable
    {
        private readonly Action _action;

        public DisposableSubscription(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}