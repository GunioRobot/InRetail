using System;
using InRetail.Shell;
using InRetail.UiCore;
using Moq;
using Microsoft.Practices.Composite.Regions;

namespace Tests.InRetail.UserInterface.ApplicationShell
{
    public class When_opening_screen_using_screen_subject : BaseTestFixture<ScreenConductor>
    {
        private TestScreen screen;

        protected override void SetupDependencies()
        {
            screen = new TestScreen();
            OnDependency<IScreenFactory>().Setup(x => x.Build<TestScreen>()).Returns(screen);
            
        }

        protected override void When()
        {
            SubjectUnderTest.OpenScreen(new TestScreenSubject<TestScreen>());
        }

        [Then]
        public void Then_created_screen_should_added_in_screens_collection()
        {
            OnDependency<IScreenCollection>().Verify(x => x.Add(screen), Times.Once());
        }

        [Then]
        public void Then_created_screen_should_shown_by_screen_collection()
        {
            OnDependency<IScreenCollection>().Verify(x => x.Show(screen), Times.Once());
        }

        [Then]
        public void Then_new_screen_should_created_and_activated_for_given_subject()
        {
            screen.CountOfActivateCalled.WillBe(1);
        }

    }

    public class When_opening_screen_using_screen_subject_and_screen_for_given_subject_is_already_opened_bat_is_not_active :
        BaseTestFixture<ScreenConductor>
    {
        private TestScreen screen;

        protected override void SetupDependencies()
        {
            screen = new TestScreen();
            
            OnDependency<IScreenCollection>().Setup(x => x.Active).Returns(new SomeOtherScreen());
            OnDependency<IScreenCollection>().Setup(x => x.AllScreens).Returns(new[] { screen });
        }

        protected override void When()
        {
            var sameSubject = new TestScreenSubject<TestScreen>();
            SubjectUnderTest.OpenScreen(sameSubject);
        }

        [Then]
        public void Then_screen_for_given_subject_should_activated()
        {
            screen.CountOfActivateCalled.WillBe(1);
        }

        [Then]
        public void Then_screen_should_shown_by_screen_collection()
        {
            OnDependency<IScreenCollection>().Verify(x => x.Show(screen), Times.Once());
        }
    }

    public class When_opening_screen_using_screen_subject_and_screen_for_given_subject_is_already_opened_and_active :
        BaseTestFixture<ScreenConductor>
    {
        private TestScreen screen;

        protected override void SetupDependencies()
        {
            screen = new TestScreen();
            OnDependency<IScreenCollection>().Setup(x => x.Active).Returns(screen);
            
        }

        protected override void When()
        {
            var sameSubject = new TestScreenSubject<TestScreen>();
            SubjectUnderTest.OpenScreen(sameSubject);
        }

        [Then]
        public void Then_should_exit()
        {
            screen.CountOfActivateCalled.WillBe(0);
            OnDependency<IScreenCollection>().Verify(x=>x.Add(It.IsAny<IScreen>()),Times.Never());
            OnDependency<IScreenCollection>().Verify(x => x.Show(It.IsAny<IScreen>()), Times.Never());
        }
    }
}