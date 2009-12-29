using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using InRetail.UiCore.Extensions;

namespace InRetail.UiCore.Helpers
{
    public class ObservableCollectionSynchronizer<T, U> : IDisposable
    {
        private readonly IList<U> _targetCollection;
        private readonly Func<T, U> _valueConverter;
        private readonly INotifyCollectionChanged _notifyCollection;

        public ObservableCollectionSynchronizer(IEnumerable<T> syncSourceCollection, IList<U> syncTargetCollection,
                                                Func<T, U> valueConverter)
        {
            if (syncSourceCollection == null) throw new ArgumentNullException("syncSourceCollection");
            if (syncTargetCollection == null) throw new ArgumentNullException("syncTargetCollection");
            if (valueConverter == null) throw new ArgumentNullException("valueConverter");

            _notifyCollection = syncSourceCollection as INotifyCollectionChanged;
            if (_notifyCollection == null)
                throw new ArgumentException("syncSourceCollection must implement INotifyCollectionChanged!");

            if (syncTargetCollection.Count != 0)
                throw new ArgumentException("syncTargetCollection is Already Populated!");


            _targetCollection = syncTargetCollection;
            _valueConverter = valueConverter;


            syncSourceCollection.Each(x => _targetCollection.Add(_valueConverter(x)));
            _notifyCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(collection1_CollectionChanged);
        }

        public void Dispose()
        {
            _notifyCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(collection1_CollectionChanged);
        }

        private void collection1_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _targetCollection.Insert(e.NewStartingIndex, _valueConverter((T)e.NewItems[0]));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    _targetCollection.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    U collection2 = _targetCollection[e.OldStartingIndex];
                    _targetCollection.RemoveAt(e.OldStartingIndex);
                    _targetCollection.Insert(e.NewStartingIndex, collection2);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _targetCollection.Clear();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    _targetCollection[e.NewStartingIndex] = _valueConverter((T)e.NewItems[0]);
                    break;
            }
        }
    }

}