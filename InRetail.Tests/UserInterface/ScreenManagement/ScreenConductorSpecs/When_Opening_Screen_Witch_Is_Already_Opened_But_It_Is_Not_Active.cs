using System;
using System.Collections.Generic;
using InRetail.UiCore;
using InRetail.UiCore.Screens;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public class When_Opening_Screen_Witch_Is_Already_Opened_But_It_Is_Not_Active : When_Creating_ScreenConductor
    {
        private IScreen newScreen;
        private IScreenSubject screenSubject;
        private IScreen inActiveScreen;

        public override void Given()
        {
            base.Given();
            inActiveScreen = newScreen = Moq.Mock<IScreen>();

            screenSubject = Moq.Mock<IScreenSubject>();
            screenSubject.Setup(x => x.Matches(inActiveScreen)).Returns(true);

            var someOtherScreen = Moq.Stub<IScreen>();
            screenCollection.Setup(x => x.Active).Returns(someOtherScreen);
            screenCollection.SetupGet(x => x.AllScreens).Returns(new[] { someOtherScreen, inActiveScreen });

        }

        public override void When()
        {
            base.When();
            conductor.OpenScreen(screenSubject);
        }

        [It]
        public void Should_Not_Call_ScreenSubject_To_CreateScreen_NewScreen()
        {
            screenSubject.Verify(x => x.CreateScreen(It.IsAny<IScreenFactory>()), Times.Never());
        }

        [It]
        public void Should_Activate_InActiveScreen()
        {
            inActiveScreen.Verify(x => x.Activate(screenObjectRegistry), Times.Once());
        }

        [It]
        public void Should_Not_Add_NewScreen_To_ScreenCollection()
        {
            screenCollection.Moq().Verify(x => x.Add(newScreen), Times.Never());
        }

        [It]
        public void Should_Call_ScreenCollection_To_Show_InActiveScreen()
        {
            screenCollection.Verify(x => x.Show(inActiveScreen), Times.Once());
        }
    }
}