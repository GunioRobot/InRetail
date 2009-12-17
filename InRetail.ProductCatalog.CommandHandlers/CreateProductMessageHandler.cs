using NServiceBus;
using ProductCatalogContracts;
using System;

namespace InRetail.ProductCatalog.CommandHandlers
{
    public class CreateProductMessageHandler : IHandleMessages<CreateProductMessage>
    {
        public IBus Bus { get; set; }
        public void Handle(CreateProductMessage message)
        {
            Console.WriteLine(string.Format("\nServer received Message {0}.", message.Description));
            var productCreatedEvent = new ProductCreatedEvent()
                             {
                                 Description = message.Description
                             };
            Bus.Publish(productCreatedEvent);
        }
    }
}