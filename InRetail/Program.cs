using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using InRetail.Shell;
using InRetail.Shell.StructureMap;
using InRetail.UiCore;
using InRetail.UiCore.Extensions;
using StructureMap;

namespace InRetail
{
    public class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            Thread.CurrentThread.Name = "MainThread";
            var app = new InRetail.Shell.App();
            app.InitializeComponent();

            Bootstrapper.Bootstrap();

            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run();
        }


    }
}