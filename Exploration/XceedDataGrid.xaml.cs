using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for XceedDataGrid.xaml
    /// </summary>
    public partial class XceedDataGrid : UserControl
    {
        private DataGridVirtualizingCollectionView _ModelColView;

        public XceedDataGrid()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(XceedDataGrid_Loaded);
            _ModelColView = new DataGridVirtualizingCollectionView(typeof(Product));

            var queryItems = _ModelColView.GetQueryItems();
            var queryItemCount = _ModelColView.GetQueryItemCount().Until(queryItems);

            queryItemCount.Subscribe(v =>
                                     {
                                         v.EventArgs.Count = 1;
                                     });
            queryItems.Subscribe(v => v.EventArgs.AsyncQueryInfo.EndQuery(new[] { new Product() { Description = "aaa" } }));
        }

        private void XceedDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridControl1.ItemsSource = _ModelColView;
        }
    }

    public static class XceedExtensions
    {
        public static IObservable<IEvent<QueryItemCountEventArgs>> GetQueryItemCount(
            this DataGridVirtualizingCollectionView view)
        {
            return
                Observable.FromEvent(
                    (EventHandler<QueryItemCountEventArgs> h) => new EventHandler<QueryItemCountEventArgs>(h),
                    h => { view.QueryItemCount += h; Debug.WriteLine("   -> Attached: " + h); },
                    h => { view.QueryItemCount -= h; Debug.WriteLine("   -> DeAttached: " + h); });
        }

        public static IObservable<IEvent<QueryItemsEventArgs>> GetQueryItems(
            this DataGridVirtualizingCollectionView view)
        {
            return
                Observable.FromEvent((EventHandler<QueryItemsEventArgs> h) => new EventHandler<QueryItemsEventArgs>(h),
                                     h => { view.QueryItems += h; Debug.WriteLine("   -> Attached: " + h); },
                                     h => { view.QueryItems -= h; Debug.WriteLine("   -> DeAttached: " + h); });
        }
    }
}