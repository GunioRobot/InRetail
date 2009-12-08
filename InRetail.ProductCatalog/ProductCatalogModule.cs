using InRetail.ProductCatalog.Presenters;
using InRetail.UiCore;
using Microsoft.Practices.Composite.Modularity;

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
            var subject = new SingletonScreenSubject<ModelSearchPresenter>();
            _screenConductor.OpenScreen(subject);
        }
    }
}