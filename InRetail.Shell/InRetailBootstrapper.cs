using System.Windows;
using InRetail.ProductCatalog;
using InRetail.Shell.StructureMap;
using Microsoft.Practices.Composite.Modularity;

namespace InRetail.Shell
{
    public class InRetailBootstrapper : StructureMapBootstrapper
    {
        protected override void ConfigureContainer()
        {
            Container.Configure(x => x.AddRegistry<ApplicationRegistry>());
            base.ConfigureContainer();
        }

        protected override DependencyObject CreateShell()
        {
            var presenter = Container.GetInstance<ShellPresenter>();
            IShellView view = presenter.View;
            view.ShowView();
            return view as DependencyObject;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog().AddModule(typeof (ProductCatalogModule));
        }
    }
}