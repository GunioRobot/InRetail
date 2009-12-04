using InRetail.UiCore;

namespace InRetail.Shell
{
    public class SingletonScreenSubject<TScreen> : IScreenSubject where TScreen : IScreen
    {
        public bool Matches(IScreen screen)
        {
            return screen is TScreen;
        }

        public IScreen CreateScreen(IScreenFactory factory)
        {
            return factory.Build<TScreen>();
        }
    }
}