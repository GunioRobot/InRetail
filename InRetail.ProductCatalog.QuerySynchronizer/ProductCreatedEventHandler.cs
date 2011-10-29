using System;
using InRetail.ProductCatalog.Persistence;
using InRetail.ProductCatalog.QueryModel;
using NServiceBus;
using ProductCatalogContracts;

namespace InRetail.ProductCatalog.QuerySynchronizer
{
    public class ProductCreatedEventHandler : IHandleMessages<ProductCreatedEvent>
    {
        public ProductCreatedEventHandler(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public IUnitOfWork Uow { get; private set; }

        public void Handle(ProductCreatedEvent message)
        {
            var view = new ProductDetailViewModel(){Id=Guid.NewGuid(),Description = message.Description};
            Uow.CurrentSession.Save(view);
            Uow.Commit();
            Console.WriteLine("ProductCreatedEvent Received {0}", message.Description);

        }
    }
}