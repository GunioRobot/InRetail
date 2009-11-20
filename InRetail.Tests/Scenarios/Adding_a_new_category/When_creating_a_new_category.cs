using System;
using System.Linq;
using InRetail.CommandHandlers;
using InRetail.Commands;
using InRetail.Domain.Categories;
using InRetail.Events;

namespace Tests.InRetail.Scenarios.Adding_a_new_category
{
    public class When_creating_a_new_category : CommandTestFixture<CreateCategoryCommand, CreateCategoryCommandHandler, Category>
    {
        protected override CreateCategoryCommand When()
        {
            return new CreateCategoryCommand(Guid.Empty, "Root Category");
        }

        [Then]
        public void Then_a_Category_created_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<CategoryCreatedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_id_of_the_category()
        {
            PublishedEvents.Last<CategoryCreatedEvent>().CategoryId.WillNotBe(Guid.Empty);
        }

        [Then]
        public void Then_the_published_event_will_contain_the_name_of_the_category()
        {
            PublishedEvents.Last<CategoryCreatedEvent>().Name.WillBe("Root Category");
        }
    }
}