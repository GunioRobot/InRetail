using System.Windows.Input;
using InRetail.UiCore.Controls;
using InRetail.UiCore.Extensions;

namespace InRetail.UiCore.Actions
{
    public class ScreenAction : IScreenAction
    {
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


        public bool IsPermanent { get; set; }
        public InputBinding Binding { get; set; }
        public string Name { get; set; }

        public ICommand Command
        {
            get { return Binding.Command; }
        }

        public bool ShortcutOnly { get; set; }

        public void BuildButton(ICommandBar bar)
        {
            bar.AddCommand(Name, Command);
        }


        public ScreenAction Permanent()
        {
            IsPermanent = true;
            return this;
        }
    }
}