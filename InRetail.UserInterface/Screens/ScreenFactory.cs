using StructureMap;

namespace InRetail.UserInterface.Screens
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly IContainer _container;

        public ScreenFactory(IContainer container)
        {
            _container = container;
        }

        public TScreen Build<TScreen>() where TScreen : IScreen
        {
            return _container.GetInstance<TScreen>();
        }

        public IScreen<T> Build<T>(T subject)
        {
            return _container.With(subject).GetInstance<IScreen<T>>();
        }


    }
}