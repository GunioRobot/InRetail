using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InRetail.UserInterface.Actions;
using InRetail.UserInterface.Extensions;

namespace InRetail.UserInterface.Controls
{
    public class CommandBar : StackPanel, ICommandBar
    {
        public CommandBar()
        {
            this.Horizontal();
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Right;
            Height = 30;
        }

        public void AddCommand(string text, Action action)
        {
            addButton(text, action);
        }

        public void AddCommand(string text, ICommand command, Icon icon)
        {
            this.Add<Button>(x =>
                                 {
                                     x.ToIconButton(icon, command).Text(text);
                                     x.VerticalAlignment = VerticalAlignment.Stretch;
                                     x.Height = 30;
                                     x.Margin = new Thickness(5, 0, 5, 0);
                                     x.HorizontalAlignment = HorizontalAlignment.Right;
                                 });
        }

        public void Refill(IEnumerable<ScreenAction> actions)
        {
            Children.Clear();
            actions.Where(x => !x.ShortcutOnly).Each(x => x.BuildButton(this));
        }

        private Button addButton(string text, Action action)
        {
            return this.Add<Button>(x =>
                                        {
                                            x.VerticalAlignment = VerticalAlignment.Stretch;
                                            x.Content = text;
                                            x.Click += action.ToRoutedHandler();
                                            x.Height = 30;
                                            x.Margin = new Thickness(5, 0, 5, 0);
                                            x.HorizontalAlignment = HorizontalAlignment.Right;
                                        });
        }
    }
}