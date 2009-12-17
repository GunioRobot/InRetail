using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace ProductCatalogContracts
{
    [Serializable]
    public class CreateProductMessage:IMessage
    {
        public string Description { get; set; }
    }

    [Serializable]
    public class ProductCreatedEvent : IMessage
    {
        public string Description { get; set; }
    }
}
