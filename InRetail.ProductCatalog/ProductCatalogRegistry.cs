using InRetail.ProductCatalog.Views;
using StructureMap.Configuration.DSL;

namespace InRetail.ProductCatalog
{
    public class ProductCatalogRegistry:Registry
    {
        public ProductCatalogRegistry()
        {
            ForRequestedType<IProductDetailView>().TheDefaultIsConcreteType<ProductDetailView>();
        }
    }
}