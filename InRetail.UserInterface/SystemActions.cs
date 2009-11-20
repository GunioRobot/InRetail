using System;
using System.Windows.Controls;
using System.Windows.Input;
using InRetail.UserInterface.Actions;
using InRetail.UserInterface.Messages;
using InRetail.UserInterface.Screens;

namespace InRetail.UserInterface
{
    public class SystemActions
    {
        private readonly IScreenObjectRegistry _screenObjects;

        public SystemActions(IScreenObjectRegistry screenObjects)
        {
            _screenObjects = screenObjects;
        }

        private IActionExpression Action(string name)
        {
            return _screenObjects.PermanentAction(name);
        }

        public void Register()
        {
            Action("UserScreenActivation").Bind(Key.F6).PublishEvent<UserScreenActivation>();
        }
    }
}