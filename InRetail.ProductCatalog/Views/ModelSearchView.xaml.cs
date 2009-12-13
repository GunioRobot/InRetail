using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InRetail.ProductCatalog.Views
{
    /// <summary>
    /// Interaction logic for ModelSearchView.xaml
    /// </summary>
    public partial class ModelSearchView : UserControl
    {
        private readonly IObservable<IEvent<RoutedEventArgs>> _refreshList;

        public ModelSearchView()
        {
            InitializeComponent();
            _refreshList = Observable.FromEvent<RoutedEventArgs>(btnRefresh, "Click");


           
        }

        public IObservable<IEvent<RoutedEventArgs>> RefreshList
        {
            get { return _refreshList; }
        }
    }
    
}
