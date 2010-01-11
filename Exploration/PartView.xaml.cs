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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for PartView.xaml
    /// </summary>
    public partial class PartView : UserControl
    {
        private bool isAnimating = false;

        public PartView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PartView_Loaded);
        }

        void PartView_Loaded(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }

        public void StartAnimation()
        {
            var storyboard = this.FindResource("Storyboard1") as Storyboard;
            if (storyboard != null && !isAnimating)
            {
                storyboard.Begin();
                isAnimating = true;
            }
        }
    }
}
