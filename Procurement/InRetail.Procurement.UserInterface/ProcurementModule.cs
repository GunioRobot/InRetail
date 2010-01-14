using System;
using InRetail.Procurement.UserInterface.Purchasing;
using InRetail.UiCore.Menus;
using InRetail.UiCore.Screens;
using Microsoft.Practices.Composite.Modularity;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace InRetail.Procurement.UserInterface
{
    public class ProcurementModule:IModule
    {
        private readonly IMenuRegistry _registry;
        private readonly IContainer _container;

        public ProcurementModule(IMenuRegistry registry,IContainer container)
        {
            _registry = registry;
            _container = container;
        }

        public void Initialize()
        {
            _container.Configure(x => x.AddRegistry(new ProcurementRegistry()));
            _registry.Register("Procurement").ToScreen<PurchaseOrderScreen>();
        }
    }

    public class ProcurementRegistry : Registry
    {
        public ProcurementRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<ProcurementRegistry>();
                x.WithDefaultConventions();
                x.ConnectImplementationsToTypesClosing(typeof(IScreen<>));
            });
        }
    }
}