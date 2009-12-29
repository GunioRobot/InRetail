using System;
using System.Linq;
using InRetail.UiCore;
using InRetail.UiCore.Actions;

namespace InRetail.Shell
{
    public class ScreenConductor : IScreenConductor
    {
        private readonly IScreenCollection _screens;
        private readonly IScreenFactory _screenFactory;
        private readonly IScreenObjectRegistry _screenObjectRegistry;

        public ScreenConductor(IScreenCollection screenCollection, IScreenFactory screenFactory, IScreenObjectRegistry screenObjectRegistry)
        {
            _screens = screenCollection;
            _screenFactory = screenFactory;
            _screenObjectRegistry = screenObjectRegistry;
        }

        public void Start()
        {
            
        }

        public void OpenScreen(IScreenSubject subject)
        {
            if (subject.Matches(_screens.Active))
                return;
            var screen = findScreenMatchingSubject(subject);
            if (screen == null)
                screen = createNewActiveScreen(subject);
            else
                activate(screen);
            _screens.Show(screen);
        }

        private void activate(IScreen screen)
        {
            screen.Activate(_screenObjectRegistry);
        }

        private IScreen createNewActiveScreen(IScreenSubject subject)
        {
            IScreen screen = subject.CreateScreen(_screenFactory);
            activate(screen);
            _screens.Add(screen);
            return screen;
        }

        private IScreen findScreenMatchingSubject(IScreenSubject subject)
        {
            return _screens.AllScreens.FirstOrDefault(subject.Matches);
        }

        public bool CanClose()
        {
            throw new NotImplementedException();
        }

        public void Close(IScreen screen)
        {
            throw new NotImplementedException();
        }

        public void CloseAllBut(IScreen screen)
        {
            throw new NotImplementedException();
        }

        public void CloseAll()
        {
            throw new NotImplementedException();
        }
    }
}