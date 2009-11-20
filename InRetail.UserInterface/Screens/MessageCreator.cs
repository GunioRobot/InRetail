using System.Windows;

namespace InRetail.UserInterface.Screens
{
    public class MessageCreator : IMessageCreator
    {
        public bool AskUser(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        public void ShowAlert(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
    }
}