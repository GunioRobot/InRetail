using System.Linq;
using System.Windows;
using System.Windows.Input;
using InRetail.UserInterface;
using InRetail.UserInterface.Actions;

namespace Tests.InRetail.UserInterface.Actions
{
    public class When_registering_actions : BaseTestFixture<ScreenObjectRegistry>
    {
        private bool IsActionExecuted = false;

        protected override void Given()
        {
            SubjectUnderTest.Action("transient").Bind(ModifierKeys.Control, Key.F5).To(() => { IsActionExecuted = true; });
            SubjectUnderTest.PermanentAction("permanent").Bind(Key.F5).To(() => { IsActionExecuted = true; });
        }

        protected override void When()
        {
        }

        [Then]
        public void Then_input_binding_will_be_added_to_window()
        {
            var window = OnDependency<Window>().Object;
            window.InputBindings[0].WillBe(SubjectUnderTest.Actions.First().Binding);
            window.InputBindings[1].WillBe(SubjectUnderTest.Actions.Last().Binding);
        }

        [Then]
        public void Then_screen_objects_first_action_will_be_screen_action()
        {
            SubjectUnderTest.Actions.First().Name.WillBe("transient");
            SubjectUnderTest.Actions.First().KeyString.WillBe("Control - F5");
            SubjectUnderTest.Actions.First().ShortcutOnly.WillBe(false);
            SubjectUnderTest.Actions.First().IsPermanent.WillBe(false);
            SubjectUnderTest.Actions.First().Command.WillBeOfType<ActionCommand>();
        }

        [Then]
        public void Then_screen_objects_last_action_will_be_screen_action()
        {
            SubjectUnderTest.Actions.Last().Name.WillBe("permanent");
            SubjectUnderTest.Actions.Last().KeyString.WillBe("F5");
            SubjectUnderTest.Actions.Last().ShortcutOnly.WillBe(false);
            SubjectUnderTest.Actions.Last().IsPermanent.WillBe(true);
            SubjectUnderTest.Actions.Last().Command.WillBeOfType<ActionCommand>();
        }
        [Then]
        public void And_executing_configured_screen_action_command_then_specified_action_will_be_executed()
        {
            IsActionExecuted.WillBe(false);
            SubjectUnderTest.Actions.First().Command.Execute(null);
            IsActionExecuted.WillBe(true);

            IsActionExecuted = false;
            SubjectUnderTest.Actions.Last().Command.Execute(null);
            IsActionExecuted.WillBe(true);
        }
    }

    public class When_placing_object_in_explorer_pane : BaseTestFixture<ScreenObjectRegistry>
    {
        private object _view;

        protected override void Given()
        {
            _view = new object();
        }

        protected override void When()
        {
            SubjectUnderTest.PlaceInExplorerPane(_view, "Test Header");
        }

        [Then]
        public void Then_call_shell_to_place_given_object()
        {
            OnDependency<IApplicationShell>().Verify(x => x.PlaceInExplorerPane(_view, "Test Header"));
        }
    }

    public class When_clearing_transient_objects : BaseTestFixture<ScreenObjectRegistry>
    {
        private object _view;

        protected override void Given()
        {
            SubjectUnderTest.Action("transient").Bind(ModifierKeys.Control, Key.F5).To(() => { });
            SubjectUnderTest.PermanentAction("permanent").Bind(Key.F5).To(() => { });
            _view = new object();
            SubjectUnderTest.PlaceInExplorerPane(_view, "Test Header");
        }

        protected override void When()
        {
            SubjectUnderTest.ClearTransient();
        }

        [Then]
        public void Then_shell_called_to_remove_objects_from_pane()
        {
            OnDependency<IApplicationShell>().Verify(x => x.RemoveFromExplorerPane(_view));
        }

        [Then]
        public void Then_transient_actions_will_be_cleared()
        {
            SubjectUnderTest.Actions.Count().WillBe(1);
            SubjectUnderTest.Actions.First().IsPermanent.WillBe(true);
        }

        [Then]
        public void Then_transient_input_bindings_will_be_removed_from_window()
        {
            var window = OnDependency<Window>().Object;
            window.InputBindings.Count.WillBe(1);
        }
    }
}