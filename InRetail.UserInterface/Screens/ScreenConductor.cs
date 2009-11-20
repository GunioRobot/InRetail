using System.Linq;
using InRetail.UserInterface.Actions;
using InRetail.UserInterface.Eventing;
using InRetail.UserInterface.Extensions;

namespace InRetail.UserInterface.Screens
{
    public class ScreenConductor : IScreenConductor
    {
        private readonly IEventAggregator _events;
        private readonly IScreenFactory _factory;
        private readonly IMessageCreator _messageBox;
        private readonly IScreenCollection _screens;
        private readonly IShellService _shellService;

        public ScreenConductor(IEventAggregator events, IMessageCreator messageBox, IScreenFactory factory,
                               IScreenCollection screens, IShellService shellService)
        {
            _events = events;
            _events.AddListener(this);
            _messageBox = messageBox;
            _factory = factory;
            _screens = screens;
            _shellService = shellService;
        }

        public void Start() {}

        public void OpenScreen(IScreenSubject subject)
        {
            if (subject.Matches(_screens.Active))
            {
                return;
            }

            IScreen screen = findScreenMatchingSubject(subject);
            if (screen == null)
            {
                screen = createNewActiveScreen(subject);
            }
            else
            {
                activate(screen);
            }

            _screens.Show(screen);
        
        }
        public void CloseAllBut(IScreen screen)
        {
            _screens.AllScreens.Where(s => s != screen).Each(x => removeScreen(x));
            activateCurrentScreen();
        }

        public void CloseAll()
        {
            _screens.AllScreens.Each(x => removeScreen(x));
        }

        public bool CanClose()
        {
            var token = new CloseToken();
            _events.SendMessage<ICloseable>(x => x.AddCanCloseMessages(token));

            bool returnValue = true;
            if (token.Messages.Length > 0)
            {
                string userMessage = string.Join("\n", token.Messages);
                returnValue = _messageBox.AskUser("CAN_CLOSE_TITLE", userMessage);
            }

            if (returnValue)
            {
                _events.SendMessage<ICloseable>(x => x.PerformShutdown());
            }

            return returnValue;
        }

        public virtual void Close(IScreen screen)
        {
            if (removeScreen(screen))
            {
                activateCurrentScreen();
            }
        }

        private void activate(IScreen screen)
        {
            _shellService.ActivateScreen(screen);
        }

        private void activateCurrentScreen()
        {
            IScreen screen = _screens.Active;
            if (screen != null)
            {
                activate(screen);
            }
        }

        private IScreen createNewActiveScreen(IScreenSubject subject)
        {
            IScreen screen = subject.CreateScreen(_factory);
            activate(screen);
            _screens.Add(screen);
            return screen;
        }

        private IScreen findScreenMatchingSubject(IScreenSubject subject)
        {
            return _screens.AllScreens.FirstOrDefault(subject.Matches);
        }

        private bool removeScreen(IScreen screen)
        {
            if (!screen.CanClose()) return false;

            _events.RemoveListener(screen);
            _screens.Remove(screen);

            _shellService.ClearTransient();

            return true;
        }
    }
}