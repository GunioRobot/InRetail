namespace InRetail.UiCore.Dialogs
{
    public interface IDialogLauncher
    {
        void Launch<COMMAND>(COMMAND command);
        void Launch<COMMAND>();
    }
}