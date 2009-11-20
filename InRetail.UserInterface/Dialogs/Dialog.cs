using System.Windows;

namespace InRetail.UserInterface.Dialogs
{
    public class Dialog : Window
    {
        public Dialog(Window top, ICommandDialog child)
        {
            ResizeMode = ResizeMode.CanResizeWithGrip;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ShowInTaskbar = false;

            Title = child.Title;
            Content = child;
            Owner = top;
        }
    }
}