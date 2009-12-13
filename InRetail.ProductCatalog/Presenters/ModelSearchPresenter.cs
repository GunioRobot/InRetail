using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using InRetail.ProductCatalog.Views;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Extensions;
using ProductCatalogModel;

namespace InRetail.ProductCatalog.Presenters
{
    public static class WpfExt
    {
        public static Panel Add<TControl>(this Panel panel, Action<TControl> configure)
            where TControl : UIElement, new()
        {
            var control = new TControl();
            configure(control);
            panel.Children.Add(control);
            return panel;
        }
    }

    public class ModelSearchPresenter<T> : IScreen where T : class
    {
        private readonly object _view = new ModelSearchView();
        private IRepository<T> _repository;
        private IDisposable _disposable;

        public void Dispose()
        {
        }

        public object View
        {
            get { return _view; }
        }

        public string Title
        {
            get { return ""; }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {
            var collection = new ObservableCollection<T>();

            _view.As<FrameworkElement>().DataContext = collection;
            var q = from r in _view.As<ModelSearchView>().RefreshList
                    from d in new Repository<T>().ObservableModels.Until(_view.As<ModelSearchView>().RefreshList.Skip(1))
                    select d;
            _view.As<ModelSearchView>().RefreshList.Subscribe(_ => collection.Clear());
            q.Subscribe(v => collection.Add(v));
            
            
        }

        public bool CanClose()
        {
            return true;
        }
    }
    public class SyncContext : SynchronizationContext
    {

    }
    public class MyCollectionView : ICollectionView
    {
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool Contains(object item)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public IDisposable DeferRefresh()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToFirst()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToLast()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToNext()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToPrevious()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentTo(object item)
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToPosition(int position)
        {
            throw new NotImplementedException();
        }

        public CultureInfo Culture { get; set; }
        public IEnumerable SourceCollection { get; private set; }
        public Predicate<object> Filter { get; set; }
        public bool CanFilter { get; private set; }
        public SortDescriptionCollection SortDescriptions { get; private set; }
        public bool CanSort { get; private set; }
        public bool CanGroup { get; private set; }
        public ObservableCollection<GroupDescription> GroupDescriptions { get; private set; }
        public ReadOnlyObservableCollection<object> Groups { get; private set; }
        public bool IsEmpty { get; private set; }
        public object CurrentItem { get; private set; }
        public int CurrentPosition { get; private set; }
        public bool IsCurrentAfterLast { get; private set; }
        public bool IsCurrentBeforeFirst { get; private set; }
        public event CurrentChangingEventHandler CurrentChanging;
        public event EventHandler CurrentChanged;
    }

    public class PagingEnabledCollectionView : ICollectionView, IItemProperties
    {
        private ListCollectionView _collectionView;
        private MySortDescriptionCollection collection;

        public PagingEnabledCollectionView(IList list)
        {
            _collectionView = new ListCollectionView(list);
        }

        public IEnumerator GetEnumerator()
        {
            return _collectionView.As<ICollectionView>().GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _collectionView.As<ICollectionView>().CollectionChanged += value; }
            remove { _collectionView.As<ICollectionView>().CollectionChanged -= value; }
        }

        public bool Contains(object item)
        {
            return _collectionView.Contains(item);
        }

        public void Refresh()
        {
            _collectionView.Refresh();
        }

        public IDisposable DeferRefresh()
        {
            return _collectionView.DeferRefresh();
        }

        public bool MoveCurrentToFirst()
        {
            return _collectionView.MoveCurrentToFirst();
        }

        public bool MoveCurrentToLast()
        {
            return _collectionView.MoveCurrentToLast();
        }

        public bool MoveCurrentToNext()
        {
            return _collectionView.MoveCurrentToNext();
        }

        public bool MoveCurrentToPrevious()
        {
            return _collectionView.MoveCurrentToPrevious();
        }

        public bool MoveCurrentTo(object item)
        {
            return _collectionView.MoveCurrentTo(item);
        }

        public bool MoveCurrentToPosition(int position)
        {
            return _collectionView.MoveCurrentToPosition(position);
        }

        public CultureInfo Culture
        {
            get { return _collectionView.Culture; }
            set { _collectionView.Culture = value; }
        }

        public IEnumerable SourceCollection
        {
            get { return _collectionView.SourceCollection; }
        }

        public Predicate<object> Filter
        {
            get { return _collectionView.Filter; }
            set { _collectionView.Filter = value; }
        }

        public bool CanFilter
        {
            get { return _collectionView.CanFilter; }
        }

        public SortDescriptionCollection SortDescriptions
        {
            get
            {
                if (collection == null)
                    collection = new MySortDescriptionCollection(_collectionView.SortDescriptions);
                return collection;
            }
        }

        public bool CanSort
        {
            get { return _collectionView.CanSort; }
        }

        public bool CanGroup
        {
            get { return _collectionView.CanGroup; }
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get { return _collectionView.GroupDescriptions; }
        }

        public ReadOnlyObservableCollection<object> Groups
        {
            get { return _collectionView.Groups; }
        }

        public bool IsEmpty
        {
            get { return _collectionView.IsEmpty; }
        }

        public object CurrentItem
        {
            get { return _collectionView.CurrentItem; }
        }

        public int CurrentPosition
        {
            get { return _collectionView.CurrentPosition; }
        }

        public bool IsCurrentAfterLast
        {
            get { return _collectionView.IsCurrentAfterLast; }
        }

        public bool IsCurrentBeforeFirst
        {
            get { return _collectionView.IsCurrentBeforeFirst; }
        }

        public event CurrentChangingEventHandler CurrentChanging
        {
            add { _collectionView.As<ICollectionView>().CurrentChanging += value; }
            remove { _collectionView.As<ICollectionView>().CurrentChanging -= value; }
        }

        public event EventHandler CurrentChanged
        {
            add { _collectionView.As<ICollectionView>().CurrentChanged += value; }
            remove { _collectionView.As<ICollectionView>().CurrentChanged -= value; }
        }

        public ReadOnlyCollection<ItemPropertyInfo> ItemProperties
        {
            get { return _collectionView.ItemProperties; }
        }
    }

    public class MySortDescriptionCollection : SortDescriptionCollection
    {
        public MySortDescriptionCollection(SortDescriptionCollection descriptions)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(MySortDescriptionCollection_CollectionChanged);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            Debug.WriteLine(this + string.Format(".ClearItems()"));
        }

        protected override void InsertItem(int index, SortDescription item)
        {
            base.InsertItem(index, item);
            Debug.WriteLine(this + string.Format(".InsertItem({0}, {1})", index, item));
        }

        private void MySortDescriptionCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("Action:" + e.Action);
            if (e.NewItems != null)
                foreach (object list in e.NewItems)
                {
                    Debug.WriteLine("New:" + list);
                }
            if (e.OldItems != null)
                foreach (object list in e.OldItems)
                {
                    Debug.WriteLine("Old:" + list);
                }
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            Debug.WriteLine(this + string.Format(".RemoveItem({0})", index));
        }

        protected override void SetItem(int index, SortDescription item)
        {
            base.SetItem(index, item);
            Debug.WriteLine(this + string.Format(".SetItem({0}, {1})", index, item));
        }
    }
}