using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        
            Observable.Context = SynchronizationContexts.CurrentDispatcher;
            list.ItemsSource = _messages;
        }

        private void onHideShowClick(object sender, RoutedEventArgs e)
        {
            grid.Visibility = grid.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        readonly ObservableCollection<NotificationMessage> _messages=new ObservableCollection<NotificationMessage>();

        private void onSetUnsetDataContext(object sender, RoutedEventArgs e)
        {

            

            var collection = new ObservableCollection<Product>();
            grid.ItemsSource = collection;
            new SlowEmumerabel().ToObservable()
                .Throttle(TimeSpan.FromMilliseconds(90))
                //.Sample(TimeSpan.FromMilliseconds(1000))
                .Subscribe(v=>collection.Insert(0,v));

            //if (grid.ItemsSource != null)
            //{
            //    grid.ItemsSource = null;
            //    return;
            //}
            //var view = new MyCollectionView(new Products());
            //view.Where(x => !x.Message.Contains("CountX")).Subscribe((v) => { 
                
            //    _messages.Insert(0,v);
                                                                               
            //});
            //grid.ItemsSource = view;
        }

        private void onClear(object sender, RoutedEventArgs e)
        {
            _messages.Clear();
        }
    }

    public class SlowEmumerabel:IEnumerable<Product>
    {
        public IEnumerator<Product> GetEnumerator()
        {
            return new SlowEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class SlowEnumerator : IEnumerator<Product>
        {
            private int _i;
            private Random _random;

            public SlowEnumerator()
            {
                _i = 0;
                _random = new Random(DateTime.Now.Millisecond);
            }
            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                Thread.Sleep(_random.Next(20,100));
                _i++;
                return _i<500;
            }

            public void Reset()
            {
                _i = 0;
            }

            public Product Current
            {
                get {
                    return new Product() {Id = _i, Description = "Product " + _i, Price = _i ^ 2};
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}