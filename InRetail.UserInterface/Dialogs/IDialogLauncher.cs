namespace InRetail.UserInterface.Dialogs
{
    public interface IDialogLauncher
    {
        void Launch<TCommand>(TCommand command);
        void Launch<TCommand>();
    }
}