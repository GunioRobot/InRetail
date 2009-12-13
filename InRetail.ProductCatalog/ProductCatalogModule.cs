using InRetail.ProductCatalog.Presenters;
using InRetail.UiCore;
using Microsoft.Practices.Composite.Modularity;
using ProductCatalogModel;

namespace InRetail.ProductCatalog
{
    public class ProductCatalogModule : IModule
    {
        private readonly IScreenConductor _screenConductor;

        public ProductCatalogModule(IScreenConductor screenConductor)
        {
            _screenConductor = screenConductor;
        }

        public void Initialize()
        {
            var subject = new SingletonScreenSubject<ModelSearchPresenter<ProductDetailViewModel>>();
            _screenConductor.OpenScreen(subject);
        }
    }
}