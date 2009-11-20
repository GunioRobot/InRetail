using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InRetail.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        void Execute(TCommand command);
    }
}
