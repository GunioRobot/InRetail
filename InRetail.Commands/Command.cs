using System;

namespace InRetail.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid Id { get; private set; }

        public Command(Guid id)
        {
            Id = id;
        }
    }
}