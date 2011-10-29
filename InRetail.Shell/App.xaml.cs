using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using InRetail.Shell.StructureMap;
using InRetail.UiCore;
using InRetail.UiCore.Actions;
using InRetail.UiCore.Extensions;
using StructureMap;

namespace InRetail.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Observable.Context = SynchronizationContexts.CurrentDispatcher;


        }


    }
}