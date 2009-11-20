using System;
using InRetail.Events.Products;
using InRetail.EventStore.Storage.Memento;

namespace InRetail.Domain.Products
{
    public class Product : BaseAggregateRoot, IOrginator
    {
        private string _name;

        public Product()
        {
            registerEvents();
        }

        private Product(string name) : this()
        {
            Apply(new ProductCreatedEvent(Guid.NewGuid(), name));
        }

        public IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }

        public static Product CreateNew(string productName)
        {
            return new Product(productName);
        }

        private void onProductCreated(ProductCreatedEvent e)
        {
            Id = e.ProductId;
            _name = e.Name;
        }

        private void registerEvents()
        {
            RegisterEvent<ProductCreatedEvent>(onProductCreated);
        }
    }
}