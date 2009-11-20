using System;
using InRetail.Events;
using InRetail.EventStore.Storage.Memento;

namespace InRetail.Domain.Categories
{
    public class Category : BaseAggregateRoot, IOrginator
    {
        private string _name;

        public Category()
        {
            registerEvents();
        }

        private Category(string name)
            : this()
        {
            Apply(new CategoryCreatedEvent(Guid.NewGuid(), name));
        }

        IMemento IOrginator.CreateMemento()
        {
            throw new NotImplementedException();
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }

        public static Category CreateNew(string name)
        {
            return new Category(name);
        }

        private void onCategoryCreated(CategoryCreatedEvent e)
        {
            Id = e.CategoryId;
            _name = e.Name;
        }

        private void registerEvents()
        {
            RegisterEvent<CategoryCreatedEvent>(onCategoryCreated);
        }
    }
}