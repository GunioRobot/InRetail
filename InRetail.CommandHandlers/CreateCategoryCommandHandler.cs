using System;
using InRetail.Commands;
using InRetail.Domain.Categories;
using InRetail.EventStore;

namespace InRetail.CommandHandlers
{
    public class CreateCategoryCommandHandler:ICommandHandler<CreateCategoryCommand>
    {
        private readonly IDomainRepository _repository;

        public CreateCategoryCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateCategoryCommand command)
        {
            var category = Category.CreateNew(command.CategoryName);
            _repository.Add(category);
        }
    }
}