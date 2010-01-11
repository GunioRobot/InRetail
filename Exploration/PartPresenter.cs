using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Unity;

namespace Exploration
{
    public class PartPresenter
    {
        private readonly IUnityContainer _container;
        private readonly RegionManager _regionManager;
        public PartPresenterView View { get; set; }
        Lazy<DelegateCommand<object>> _addPartCommand;

        public PartPresenter(PartPresenterView view, IUnityContainer container)
        {
            _container = container;
            View = view;
            View.DataContext = this;
            _regionManager = new RegionManager();
            RegionManager.SetRegionManager(View, _regionManager);

            _addPartCommand = new Lazy<DelegateCommand<object>>(() => new DelegateCommand<object>(AddPartExecuted));
            Action<int> add = (i) =>
            {
                var region = _regionManager.Regions["Page1Content" + i];
                if (region.Views.Count() == 0)
                {
                    var partView = _container.Resolve<PartView>();

                    region.Add(partView);
                    region.Activate(partView);
                    
                }
            };

            add(1);
            add(2);
            add(3);
        }

        private void AddPartExecuted(object o)
        {
            _regionManager.Regions["TabRegion"].Add(_container.Resolve<PartView>());



        }

        public ICommand AddPartCommand
        {
            get
            {

                return _addPartCommand.Value;
            }
        }
    }
}