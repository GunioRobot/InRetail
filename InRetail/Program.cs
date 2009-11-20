using InRetail.UserInterface;

namespace InRetail
{
    public class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            var shell= ApplicationBootStrapper.BootStrapShell();
            shell.Title = "In Retail";

            var application = new App();
            application.Run(shell);
        }
    }
}