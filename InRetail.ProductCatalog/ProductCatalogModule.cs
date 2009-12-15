using InRetail.ProductCatalog.Presenters;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using Microsoft.Practices.Composite.Modularity;
using ProductCatalogModel;
using System.Windows.Input;

namespace InRetail.ProductCatalog
{
    public class ProductCatalogModule : IModule
    {
        private readonly IScreenConductor _screenConductor;
        private readonly IScreenObjectRegistry _screenObjectRegistry;

        public ProductCatalogModule(IScreenConductor screenConductor, IScreenObjectRegistry actionRegistry)
        {
            _screenConductor = screenConductor;
            _screenObjectRegistry = actionRegistry;
        }

        public void Initialize()
        {
            var subject = new SingletonScreenSubject<ModelSearchPresenter<ProductDetailViewModel>>();

            _screenObjectRegistry.PermanentAction("Create Product").Bind(Key.F2).ToDialog<CreateProductDialog>();
            _screenConductor.OpenScreen(subject);
        }
    }
}