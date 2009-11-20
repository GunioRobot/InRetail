using System;

namespace InRetail.Events.Products
{
    [Serializable]
    public class ProductCreatedEvent : DomainEvent
    {
        public ProductCreatedEvent(Guid productId, string name)
        {
            ProductId = productId;
            Name = name;
        }

        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
    }
}