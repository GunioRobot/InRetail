using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InRetail.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
