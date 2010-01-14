using System.Windows.Input;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.AssertHelpers
{
    public static class CommandExtensions
    {
        public static void Click(this ICommand cmd)
        {
            cmd.ShouldBeEnabled();
            cmd.Execute(null);
        }
    }
}