using System;
using InRetail.ProductCatalog.Persistence;
using NServiceBus;
using NServiceBus.Grid.MessageHandlers;
using NServiceBus.Sagas.Impl;
using Configure = NServiceBus.Configure;
using StructureMap;

namespace InRetail.ProductCatalog.QuerySynchronizer
{
    class EndpointConfig : IConfigureThisEndpoint, AsA_Server, ISpecifyMessageHandlerOrdering, IWantCustomInitialization,IWantCustomLogging
    {
        public void SpecifyOrder(Order order)
        {
            order.Specify(First<GridInterceptingMessageHandler>.Then<SagaMessageHandler>());
        }

        public void Init()
        {
            ObjectFactory.Configure(c => c.AddRegistry<NHibernateRegistry>());
            NServiceBus.Configure.With().StructureMapBuilder(ObjectFactory.Container).XmlSerializer();
        }
    }

}