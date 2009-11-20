using System;
using System.Windows;

namespace InRetail.UserInterface
{
    public class MainWindow : Window
    {
        public MainWindow(IApplicationShell shell)
        {
            Content = shell;

            CanClose = (() => true);
            Closing += (s, e) => e.Cancel = !CanClose();

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowState = WindowState.Maximized;
            Height = 800;
            Width = 1200;
        }

        public Func<bool> CanClose { get; set; }
    }
}