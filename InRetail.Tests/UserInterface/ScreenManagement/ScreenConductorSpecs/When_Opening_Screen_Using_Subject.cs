using System;
using System.Collections.Generic;
using InRetail.UiCore.Screens;
using Moq;

namespace Tests.InRetail.UserInterface.ScreenManagement.ScreenConductorSpecs
{
    public class When_Opening_Screen_Using_Subject : When_Creating_ScreenConductor
    {
        IScreen screenToOpen;
        IScreenSubject screenSubject;

        public override void Given()
        {
            base.Given();
            screenToOpen = new Mock<IScreen>().Object;

            screenSubject = Moq.Mock<IScreenSubject>();
            screenSubject.Setup(x => x.CreateScreen(screenFactory)).Returns(screenToOpen);
        }

        public override void When()
        {
            base.When();
            conductor.OpenScreen(screenSubject);
        }

        [It]
        public void Should_Activate_Screen_And_Pass_ScreenObjectRegistry_To_Screen_To_Register_ScreenActions()
        {
            screenToOpen.Verify(x => x.Activate(screenObjectRegistry), Times.Once());
        }

        [It]
        public void Should_Add_Screen_To_Screen_Collection()
        {
            screenCollection.Verify(x => x.Add(screenToOpen), Times.Once());
        }

        [It]
        public void Should_Call_Screen_Collection_To_Show_Screen()
        {
            screenCollection.Verify(x => x.Show(screenToOpen), Times.Once());
        }
    }
}