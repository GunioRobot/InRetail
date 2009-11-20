using InRetail.UserInterface.Controls;
using InRetail.UserInterface.Screens;

namespace InRetail.UserInterface.Actions
{
    public class ShellService : IShellService
    {
        private readonly ICommandBar _commands;
        private readonly IScreenObjectRegistry _registry;

        public ShellService(IScreenObjectRegistry registry, ICommandBar commands)
        {
            _registry = registry;
            _commands = commands;
        }

        public void ActivateScreen(IScreen screen)
        {
            _registry.ClearTransient();
            screen.Activate(_registry);
            refill();
        }

        public void ClearTransient()
        {
            _registry.ClearTransient();
            refill();
        }

        public void Start()
        {
            refill();
        }

        private void refill()
        {
            _commands.Refill(_registry.Actions);
        }
    }
}