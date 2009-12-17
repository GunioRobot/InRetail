using NServiceBus;
using StructureMap.Configuration.DSL;

namespace ProductCatalogContracts
{
    public class ProductCatalogContractsRegistry:Registry
    {
        public ProductCatalogContractsRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<CreateProductMessage>();
                x.Include(t =>
                {
                    bool @from = typeof(IMessage).IsAssignableFrom(t);
                    return @from;
                });
            });
        }
    }
}