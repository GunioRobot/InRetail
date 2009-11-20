using System.Windows.Input;
using InRetail.UserInterface.Controls;
using InRetail.UserInterface.Extensions;

namespace InRetail.UserInterface.Actions
{
    public class ScreenAction : IScreenAction
    {
        public ScreenAction()
        {
            Icon = Icon.Unknown;
        }

        public string KeyString
        {
            get
            {
                var gesture = Binding.Gesture.As<KeyGesture>();
                string returnValue = string.Empty;
                if (gesture.Modifiers != ModifierKeys.None)
                {
                    returnValue += gesture.Modifiers + " - ";
                }

                return returnValue + gesture.Key;
            }
        }

        #region IScreenAction Members

        public bool IsPermanent { get; set; }
        public InputBinding Binding { get; set; }
        public string Name { get; set; }
        public Icon Icon { get; set; }

        public ICommand Command { get { return Binding.Command; } }

        public bool ShortcutOnly { get; set; }

        public void BuildButton(ICommandBar bar)
        {
            bar.AddCommand(Name, Command, Icon);
        }

        #endregion

        public ScreenAction Permanent()
        {
            IsPermanent = true;
            return this;
        }
    }
}