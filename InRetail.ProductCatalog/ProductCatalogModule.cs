using InRetail.UiCore;
using Microsoft.Practices.Composite.Modularity;
using StructureMap;
using ScreenAction=InRetail.UiCore.Actions.ScreenAction;

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
            //_container.Configure(x => x.AddRegistry<ProductCatalogRegistry>());
            //_shellService.AddModuleAction(Actions.ToScreen<ProductDetailViewModel>());

            //    AddActions().ToScreen<ProductDetailViewModel>().

            //_shellService
            //    .AddModuleAction(
            //    new CompositeScreenAction()
            //    .Add(new ScreenAction())
            //    .Add(new ScreenAction())
            //    .Add(new CompositeScreenAction().Add(new ScreenAction())));
        }
    }
}