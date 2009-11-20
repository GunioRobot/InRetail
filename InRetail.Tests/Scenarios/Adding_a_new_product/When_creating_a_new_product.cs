using System;
using System.Linq;
using InRetail.CommandHandlers;
using InRetail.Commands;
using InRetail.Domain.Products;
using InRetail.Events.Products;

namespace Tests.InRetail.Scenarios.Adding_a_new_product
{
    public class When_creating_a_new_product : CommandTestFixture<CreateProductCommand, CreateProductCommandHandler, Product>
    {
        protected override CreateProductCommand When()
        {
            return new CreateProductCommand(Guid.Empty, "Test Product");
        }

        [Then]
        public void Then_a_product_created_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ProductCreatedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_new_product_id()
        {
            PublishedEvents.Last<ProductCreatedEvent>().ProductId.WillNotBe(Guid.Empty);
        }

        [Then]
        public void Then_the_published_event_will_contain_the_name_of_the_client()
        {
            PublishedEvents.Last<ProductCreatedEvent>().Name.WillBe("Test Product");
        }
    }
}