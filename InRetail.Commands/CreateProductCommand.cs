using System;

namespace InRetail.Commands
{
    [Serializable]
    public class CreateProductCommand : Command
    {
        public string Name { get; private set; }

        public CreateProductCommand(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}