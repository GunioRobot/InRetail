using System;
using System.Windows.Controls;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using System.Windows;
using InRetail.UiCore.Extensions;

namespace InRetail.ProductCatalog.Presenters
{
    public static class WpfExt
    {
        public static Panel Add<TControl>(this Panel panel, Action<TControl> configure) where TControl : UIElement, new()
        {
            var control = new TControl();
            configure(control);
            panel.Children.Add(control);
            return panel;
        }
    }
    public class ModelSearchPresenter : IScreen
    {
        private readonly object _view = new StackPanel()
            .Configure(x=>x.Orientation=Orientation.Horizontal)
            .Add<Button>(x => x.Content = "btn1")
            .Add<Button>(x => x.Content = "btn2")
            .Add<Button>(x => x.Content = "btn3");

        public void Dispose()
        {

        }

        public object View
        {
            get
            {

                return _view;
            }
        }

        public string Title
        {
            get { return ""; }
        }

        public void Activate(IScreenObjectRegistry screenObjects)
        {

        }

        public bool CanClose()
        {
            return true;
        }
    }
}