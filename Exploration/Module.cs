using System;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Exploration
{
    public class Module:IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public Module(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            var region = _regionManager.Regions["ListRegion"];

            region.Add(_container.Resolve<PartPresenter>());
            region.Add(_container.Resolve<PartPresenter>());

        }
    }
}