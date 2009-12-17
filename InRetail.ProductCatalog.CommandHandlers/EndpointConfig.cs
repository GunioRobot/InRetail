using System;
using NServiceBus;
using NServiceBus.Grid.MessageHandlers;
using NServiceBus.Sagas.Impl;
using Configure=NServiceBus.Configure;

namespace InRetail.ProductCatalog.CommandHandlers
{
    class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, ISpecifyMessageHandlerOrdering
    {
        public void SpecifyOrder(Order order)
        {
            order.Specify(First<GridInterceptingMessageHandler>.Then<SagaMessageHandler>());
        }
    }
}