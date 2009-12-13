using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for CanvasDraw.xaml
    /// </summary>
    public partial class CanvasDraw : UserControl
    {
        public CanvasDraw()
        {
            InitializeComponent();

            var mouseMoves = from mm in mainCanvas.GetMouseMove()
                             let location = mm.EventArgs.GetPosition(mainCanvas)
                             select new { location.X, location.Y };

            var mouseDiffs = mouseMoves.Skip(1).Zip(mouseMoves, (l, r) => new { X1 = l.X, Y1 = l.Y, X2 = r.X, Y2 = r.Y });

            var mouseDrag = from _ in mainCanvas.GetMouseLeftButtonDown()
                            from md in mouseDiffs.Until(
                                mainCanvas.GetMouseLeftButtonUp())
                            select md;
            var mouseSub = mouseDrag.Subscribe(item =>
            {
                var line = new Line
                {
                    Stroke = Brushes.LightSteelBlue,
                    X1 = item.X1,
                    X2 = item.X2,
                    Y1 = item.Y1,
                    Y2 = item.Y2,
                    StrokeThickness = 5
                };
                mainCanvas.Children.Add(line);
            });
        }
    }
    public static class EventExtensions
    {
        public static IObservable<IEvent<MouseEventArgs>> GetMouseMove(
            this IInputElement inputElement)
        {
            return Observable.FromEvent(
                (EventHandler<MouseEventArgs> h) => new MouseEventHandler(h),
                h => inputElement.MouseMove += h,
                h => inputElement.MouseMove -= h);
        }
        public static IObservable<IEvent<MouseButtonEventArgs>> GetMouseLeftButtonDown(
    this IInputElement inputElement)
        {
            return Observable.FromEvent(
                (EventHandler<MouseButtonEventArgs> h) => new MouseButtonEventHandler(h),
                h => inputElement.MouseLeftButtonDown += h,
                h => inputElement.MouseLeftButtonDown -= h);
        }
        public static IObservable<IEvent<MouseButtonEventArgs>> GetMouseLeftButtonUp(
    this IInputElement inputElement)
        {
            return Observable.FromEvent(
                (EventHandler<MouseButtonEventArgs> h) => new MouseButtonEventHandler(h),
                h => inputElement.MouseLeftButtonUp += h,
                h => inputElement.MouseLeftButtonUp -= h);
        }
    }
}
