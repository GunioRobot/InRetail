using System;
using InRetail.UiCore;
using StructureMap;

namespace InRetail.Shell
{
    public class ScreenFactory:IScreenFactory
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

        public IScreen<TSubject> Build<TSubject>(TSubject subject)
        {
            return _container.With(subject).GetInstance<IScreen<TSubject>>();
        }
    }
}