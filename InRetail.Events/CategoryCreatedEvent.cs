using System;

namespace InRetail.Events
{
    [Serializable]
    public class CategoryCreatedEvent : DomainEvent
    {
        public CategoryCreatedEvent(Guid categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
    }
}