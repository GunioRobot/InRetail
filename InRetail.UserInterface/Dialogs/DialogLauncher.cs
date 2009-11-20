using StructureMap;

namespace InRetail.UserInterface.Dialogs
{
    public class DialogLauncher : IDialogLauncher
    {
        private readonly IContainer _container;

        public DialogLauncher(IContainer container)
        {
            _container = container;
        }


        public void Launch<TCommand>(TCommand command)
        {
            var dialog = BuildDialog(command);
            dialog.ShowDialog();
        }

        public void Launch<TCommand>()
        {
            var command = _container.GetInstance<TCommand>();
            Launch(command);
        }

        public Dialog BuildDialog<TCommand>(TCommand command)
        {
            var control = _container.With(command).GetInstance<ICommandDialog<TCommand>>();
            return _container.With<ICommandDialog>(control).GetInstance<Dialog>();
        }
    }
}