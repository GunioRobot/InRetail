using InRetail.Shell;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Screens;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public class When_Creating_ScreenConductor : Specification
    {
        protected ScreenConductor conductor;
        protected IScreenObjectRegistry screenObjectRegistry;
        protected IScreenCollection screenCollection;
        protected IScreenFactory screenFactory;

        public override void Given()
        {
            screenCollection = Moq.Mock<IScreenCollection>();
            screenObjectRegistry=Moq.Mock<IScreenObjectRegistry>();
            screenFactory = Moq.Mock<IScreenFactory>();
        }

        public override void When()
        {
            conductor = new ScreenConductor(screenCollection, screenFactory, screenObjectRegistry);
        }

        [It]
        public void Just_Initializes_Private_Fields()
        {

        }
    }
}