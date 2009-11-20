using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using InRetail.UserInterface.Controls;

namespace InRetail.UserInterface.Extensions
{
    public static class WpfControlsExtensions
    {
        public static T Add<T>(this Panel panel, Action<T> action) where T : UIElement, new()
        {
            var element = panel.Add<T>();
            action(element);
            return element;
        }
        public static DockPanel With(this DockPanel panel, object element)
        {
            panel.Children.Add(element.As<UIElement>());
            return panel;
        }

        public static MenuItem AddItem(this ContextMenu menu, string text, Action action)
        {
            var item = new MenuItem
            {
                Header = text
            };

            item.Click += action.ToRoutedHandler();
            menu.Items.Add(item);

            return item;
        }
        public static T Add<T>(this Panel panel) where T : UIElement, new()
        {
            var t = new T();
            panel.Children.Add(t);
            return t;
        }

        public static StackPanel AddText(this StackPanel panel, string text, Action<Label> action)
        {
            var label = panel.Add<Label>();
            label.Content = text;

            action(label);

            return panel;
        }

        public static StackPanel Horizontal(this StackPanel panel)
        {
            panel.Orientation = Orientation.Horizontal;
            return panel;
        }

        public static StackPanel IconButton(this StackPanel panel, Icon icon, Action action,
                                            Action<ButtonExpression> configureButton)
        {
            configureButton(panel.Add<Button>().ToIconButton(icon, action));
            return panel;
        }

        public static void SetIcon(this Image image, Icon icon)
        {
            image.Tag = icon;

            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = icon.ImageStream();
            source.EndInit();

            image.Source = source;
        }

        public static ButtonExpression ToIconButton(this ButtonBase button, Icon icon, Action action)
        {
            button.Click += (s, e) => action();
            return button.ToIconButton(icon);
        }

        public static ButtonExpression ToIconButton(this ButtonBase button, Icon icon, ICommand command)
        {
            button.Command = command;
            return button.ToIconButton(icon);
        }

        public static ButtonExpression ToIconButton(this ButtonBase button, Icon icon)
        {
            button.ClickMode = ClickMode.Release;

            var image = button.Content as Image;
            if (image == null)
            {
                image = new Image();
                button.Content = image;

                image.Width = 25;
                image.Height = 25;
            }

            image.SetIcon(icon);

            return new ButtonExpression(button);
        }

        public static RoutedEventHandler ToRoutedHandler(this Action action)
        {
            return (s, e) => action();
        }

        public class ButtonExpression
        {
            private readonly ButtonBase _button;

            public ButtonExpression(ButtonBase button)
            {
                _button = button;
            }

            public ButtonBase Button
            {
                get { return _button; }
            }

            public ButtonExpression DoesNotAcceptTab()
            {
                _button.TabIndex = int.MaxValue;
                return this;
            }

            public ButtonExpression SmallerImages()
            {
                Image image = _button.Content as Image ?? _button.Content.As<StackPanel>().Children[0].As<Image>();
                image.Width = image.Height = 16;

                return this;
            }

            public void Text(string text)
            {
                var panel = new StackPanel
                                {
                                    Orientation = Orientation.Horizontal
                                };
                var image = _button.Content.As<Image>();
                _button.Content = panel;
                panel.Children.Add(image);
                panel.Children.Add(new Label
                                       {
                                           Content = text
                                       });
            }
        }
    }
}