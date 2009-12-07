using System.Windows;

namespace InRetail.Shell.Dialogs
{
    public class Dialog : Window
    {
        public Dialog(Window window, ICommandDialog commandDialog)
        {
            ResizeMode = ResizeMode.CanResizeWithGrip;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowInTaskbar = false;
            Title = commandDialog.Title;
            Content = commandDialog;
            Owner = window;
        }
    }
}