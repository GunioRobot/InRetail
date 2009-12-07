namespace InRetail.Shell.Dialogs
{
    public interface IDialogLauncher
    {
        void Launch<COMMAND>(COMMAND command);
        void Launch<COMMAND>();
    }
}