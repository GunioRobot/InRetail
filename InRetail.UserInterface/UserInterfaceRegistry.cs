using System.Threading;
using System.Windows;
using System.Windows.Threading;
using InRetail.UserInterface.Actions;
using InRetail.UserInterface.Eventing;
using InRetail.UserInterface.Extensions;
using InRetail.UserInterface.Screens;
using StructureMap.Configuration.DSL;

namespace InRetail.UserInterface
{
    public class UserInterfaceRegistry : Registry
    {
        public UserInterfaceRegistry()
        {
            Scan(x => x.With(new GenericConnectionScanner(typeof (IScreen<>))));

            ForRequestedType<IScreenConductor>().AsSingletons()
                .TheDefault.Is.OfConcreteType<ScreenConductor>();
            ForRequestedType<IScreenFactory>().AsSingletons()
                .TheDefault.Is.OfConcreteType<ScreenFactory>();
            ForRequestedType<IScreenCollection>().AsSingletons()
                .TheDefault.Is.OfConcreteType<ScreenCollection>();
            ForRequestedType<IShellService>().AsSingletons()
                .TheDefault.Is.OfConcreteType<ShellService>();
            ForRequestedType<IScreenObjectRegistry>().AsSingletons()
                .TheDefault.Is.OfConcreteType<ScreenObjectRegistry>();
            ForRequestedType<IMessageCreator>().AsSingletons()
                .TheDefault.Is.OfConcreteType<MessageCreator>();

            setupSynchronization();
            registerEventAggregator();

            setupShell();
        }
        private void setupSynchronization()
        {
            ForSingletonOf<SynchronizationContext>().TheDefault.Is.ConstructedBy(() =>
            {
                if (SynchronizationContext.Current == null)
                {
                    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
                }

                return SynchronizationContext.Current;
            });
        }
        private void registerEventAggregator()
        {
            ForRequestedType<IEventAggregator>().AsSingletons()
                .TheDefault.Is.OfConcreteType<EventAggregator>();
            RegisterInterceptor(new EventAggregatorInterceptor());
        }

        private void setupShell()
        {
            var shell = new Shell();
            shell.Register(this);

            ForRequestedType<Window>().TheDefault.IsThis(new MainWindow(shell));

            ForSingletonOf<IScreenConductor>()
                .TheDefault.Is.OfConcreteType<ScreenConductor>()
                .OnCreation((context, screenConductor) =>
                                {
                                    var mainWindow = context.GetInstance<Window>().As<MainWindow>();
                                    mainWindow.CanClose = screenConductor.CanClose;
                                });
        }
    }
}