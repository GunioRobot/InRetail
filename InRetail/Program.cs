

using InRetail.Shell;

namespace InRetail
{
    public class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            Bootstrapper.Bootstrap();

            var application = new InRetail.Shell.App();
            application.InitializeComponent();
            application.Run();
        }
    }
}