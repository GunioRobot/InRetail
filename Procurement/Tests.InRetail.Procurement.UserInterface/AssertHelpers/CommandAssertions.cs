using System.Diagnostics;
using System.Windows.Input;
using Xunit;

namespace Tests.InRetail.Procurement.AssertHelpers
{
    public static class CommandAssertionExtensions
    {
        [DebuggerStepThrough()]
        public static void ShouldBeDisabled(this ICommand command)
        {
            Assert.False(command.CanExecute(new object()));
        }

        [DebuggerStepThrough()]
        public static void ShouldBeEnabled(this ICommand command)
        {
            Assert.True(command.CanExecute(new object()));
        }
    }
}