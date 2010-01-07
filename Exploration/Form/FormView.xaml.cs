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

namespace Exploration.Form
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class FormView : UserControl, IFormView
    {
        public FormView()
        {
            InitializeComponent();
        }

        public void Bind(object model)
        {
            DataContext = model;
        }
    }

    public interface IFormView
    {
        void Bind(object model);
    }
}
