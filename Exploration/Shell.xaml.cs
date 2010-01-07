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
using System.Windows.Shapes;
using Exploration.Form;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Shell_Loaded);
        }

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            var view = new FormView();
            var model = new FormViewModel(view);


            model.AddPart(new PartViewModel() { ColSpan = 1 });
            model.AddPart(new PartViewModel() { ColSpan = 1 });
            model.AddPart(new PartViewModel() { ColSpan = 1 });
            model.AddPart(new PartViewModel() { ColSpan = 1 });
            Content = view;
        }
    }
}
