using System.Collections.Generic;
using InRetail.Shell;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using Moq;

namespace Tests.InRetail.UserInterface.ApplicationShell
{
    public abstract class With_Emty_Conductor : Specification
    {
        protected ScreenConductor conductor;
        protected IScreenFactory screenFactory;
        protected IScreenObjectRegistry screenObjectRegistry;
        protected IScreenCollection screenCollection;
        protected IList<object> activeScreens;

        public override void Given()
        {
            screenFactory = new Mock<IScreenFactory>().Object;
            screenObjectRegistry = new Mock<IScreenObjectRegistry>().Object;
            screenCollection = new Mock<IScreenCollection>().Object;
            conductor = new ScreenConductor(screenCollection, screenFactory, screenObjectRegistry);
        }
    }
}