namespace InRetail.Shell.Dialogs
{
    public interface ICommandDialog
    {
        string Title { get; }
    }

    public interface ICommandDialog<COMMAND> : ICommandDialog
    {
    }
}