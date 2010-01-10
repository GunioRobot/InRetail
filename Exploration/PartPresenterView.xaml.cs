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
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Unity;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for PartPresenterView.xaml
    /// </summary>
    public partial class PartPresenterView : UserControl
    {
        public PartPresenterView()
        {
            InitializeComponent();
        }

    }
    public class PartPresenter
    {
        private readonly IUnityContainer _container;
        private readonly RegionManager _regionManager;
        public PartPresenterView View { get; set; }
        Lazy<DelegateCommand<object>> _addPartCommand;

        public PartPresenter(PartPresenterView view,IUnityContainer container)
        {
            _container = container;
            View = view;
            View.DataContext = this;
            _regionManager = new RegionManager();
            RegionManager.SetRegionManager(View, _regionManager);

            _addPartCommand=new Lazy<DelegateCommand<object>>(()=>new DelegateCommand<object>(AddPartExecuted));
        }

        private void AddPartExecuted(object o)
        {
            _regionManager.Regions["TabRegion"].Add(_container.Resolve<PartView>());
        }

        public ICommand AddPartCommand {
            get
            {
                
                return _addPartCommand.Value;
            }
        }
    }
}
