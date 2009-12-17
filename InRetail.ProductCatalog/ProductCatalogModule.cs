using InRetail.ProductCatalog.Commands.CreateProduct;
using InRetail.ProductCatalog.Presenters;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using Microsoft.Practices.Composite.Modularity;
using ProductCatalogModel;
using System.Windows.Input;
using StructureMap;
using ProductCatalogContracts;

namespace InRetail.ProductCatalog
{
    public class ProductCatalogModule : IModule
    {
        private readonly IContainer _container;
        private readonly IScreenConductor _screenConductor;
        private readonly IScreenObjectRegistry _screenObjectRegistry;

        public ProductCatalogModule(IContainer container, IScreenConductor screenConductor, IScreenObjectRegistry actionRegistry)
        {
            _container = container;
            _screenConductor = screenConductor;
            _screenObjectRegistry = actionRegistry;
        }

        public void Initialize()
        {
            _container.Configure(x =>
            {
                x.AddRegistry<ProductCatalogRegistry>();
                x.AddRegistry<ProductCatalogContractsRegistry>();
            });
            var subject = new SingletonScreenSubject<ModelSearchPresenter<ProductDetailViewModel>>();
            
            
            _screenObjectRegistry.PermanentAction("Create Product").Bind(Key.F2).ToDialog<CreateProductMessage>();
            _screenConductor.OpenScreen(subject);
        }
    }
}