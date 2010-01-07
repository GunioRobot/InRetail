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
       
        public ProductCatalogModule()
        {
            
        }

        public void Initialize()
        {
        }
    }
  
}