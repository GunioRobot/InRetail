using System;
using System.Threading;
using System.Windows;
using NUnit.Framework;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class ThreadingFixture
    {
        [Test]
        public void CreatingCustomDispatcher()
        {
            Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
            Thread.Sleep(-1);
        }

        private void ThreadStartingPoint()
        {
            Window tempWindow = new Window();
            tempWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}