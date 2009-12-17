using System;
using System.Windows;
using InRetail.ProductCatalog;
using InRetail.Shell.StructureMap;
using InRetail.UiCore.Extensions;
using Microsoft.Practices.Composite.Modularity;
using StructureMap;

namespace InRetail.Shell
{
    public class InRetailBootstrapper : StructureMapBootstrapper
    {
        protected override IContainer CreateContainer()
        {
            return ObjectFactory.Container;
        }

        protected override DependencyObject CreateShell()
        {
            var presenter = Container.GetInstance<ShellPresenter>();
            IShellView view = presenter.View;
            view.ShowView();
            Container.Configure(x => x.ForSingletonOf<Window>().TheDefault.IsThis(view.As<Window>()));
            return view as DependencyObject;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog().AddModule(typeof (ProductCatalogModule));
        }
    }
}