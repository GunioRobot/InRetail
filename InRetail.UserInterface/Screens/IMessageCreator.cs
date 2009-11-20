namespace InRetail.UserInterface.Screens
{
    public interface IMessageCreator
    {
        bool AskUser(string title, string message);
        void ShowAlert(string title, string message);
    }
}