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
using InRetail.UserInterface.Controls;
using StructureMap.Configuration.DSL;

namespace InRetail.UserInterface
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : UserControl, IApplicationShell
    {
        public Shell()
        {
            InitializeComponent();
        }

        public void Register(Registry registry)
        {
            registry.ForRequestedType<IApplicationShell>().TheDefault.IsThis(this);
            registry.ForRequestedType<TabControl>().TheDefault.IsThis(mainTabs);
            registry.ForRequestedType<ICommandBar>().TheDefault.IsThis(commandBar);
        }

        public void RemoveFromExplorerPane(object view)
        {
            throw new NotImplementedException();
        }

        public void PlaceInExplorerPane(object view, string header)
        {
            throw new NotImplementedException();
        }
    }
}
