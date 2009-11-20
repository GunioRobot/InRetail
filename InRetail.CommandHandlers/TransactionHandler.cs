using System;
using InRetail.EventStore;

namespace InRetail.CommandHandlers
{
    public class TransactionHandler<TCommand, TCommandHandler>
        where TCommandHandler : ICommandHandler<TCommand>
        where TCommand : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute(TCommand command, TCommandHandler commandHandler)
        {
            try
            {
                commandHandler.Execute(command);
                _unitOfWork.Commit();
            }
            catch (Exception Ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}