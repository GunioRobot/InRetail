using System;
using InRetail.ProductCatalog.Views;
using InRetail.UiCore.Dialogs;
using ProductCatalogContracts;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using NServiceBus;
using System.Diagnostics;

namespace InRetail.ProductCatalog
{
    public class ProductCatalogRegistry : Registry
    {
        public ProductCatalogRegistry()
        {
            Scan(x =>
                         {
                             x.AssemblyContainingType<ProductCatalogRegistry>();
                             x.WithDefaultConventions();
                             x.With(new GenericConnectionScanner(typeof(ICommandDialog<>)));
                         });



            ForRequestedType<IProductDetailView>().TheDefaultIsConcreteType<ProductDetailView>();
        }
    }

    public class GenericConnectionScanner : ITypeScanner
    {
        // Fields
        private readonly Type _openType;

        // Methods
        public GenericConnectionScanner(Type openType)
        {
            this._openType = openType;
            if (!this._openType.IsGeneric())
            {
                throw new ApplicationException("This scanning convention can only be used with open generic types");
            }
        }

        public void Process(Type type, PluginGraph graph)
        {
            Type pluginType = type.FindInterfaceThatCloses(this._openType);
            if (pluginType != null)
            {
                graph.AddType(pluginType, type);
            }
        }
    }


}