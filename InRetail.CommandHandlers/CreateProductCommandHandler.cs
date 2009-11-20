using InRetail.Commands;
using InRetail.Domain.Products;
using InRetail.EventStore;

namespace InRetail.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IDomainRepository _repository;

        public CreateProductCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateProductCommand command)
        {
            var product = Product.CreateNew(command.Name);
            _repository.Add(product);
        }
    }
}