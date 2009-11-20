using System;

namespace InRetail.Commands
{
    [Serializable]
    public class CreateCategoryCommand : Command
    {
        public CreateCategoryCommand(Guid id, string categoryName) : base(id)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; private set; }
    }
}