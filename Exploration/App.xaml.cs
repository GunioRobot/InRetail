using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Exploration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {   
            string setting = ConfigurationManager.AppSettings["License"];
            Xceed.Wpf.Controls.Licenser.LicenseKey = setting;
            Xceed.Wpf.DataGrid.Licenser.LicenseKey = setting;
            base.OnStartup(e);
        

        }
    }
}
