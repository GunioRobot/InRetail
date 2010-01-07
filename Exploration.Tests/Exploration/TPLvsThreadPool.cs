using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Exploration.Tests.Exploration
{
    [TestFixture]
    public class TPLvsThreadPool
    {

        int counter;
        int processed;
        int toprocessed = 100000;
        int max;

        AutoResetEvent s_are;

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            var affinity = new IntPtr(15);
            Process.GetCurrentProcess().ProcessorAffinity = affinity;
            Process.GetCurrentProcess().PriorityClass=ProcessPriorityClass.RealTime;
            Debug.WriteLine(string.Format("ProcessorAffinity: {0}", affinity));
            s_are = new AutoResetEvent(false);
        }

        [SetUp]
        public void Setup()
        {
            Thread.Sleep(2000);
        }

        [Test]
        public void BombardThreadPool()
        {
            counter = processed = max = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < toprocessed; i++)
                ThreadPool.QueueUserWorkItem(new WaitCallback(state => act()));
            s_are.WaitOne();
            sw.Stop();
            Debug.WriteLine("ThreadPool\nMax:" + max + " Incomplete:" + counter + " Processed:" + processed + " TimeElapsed:" + sw.Elapsed);
        }

        [Test]
        public void BombardTPL()
        {
            counter = processed = max = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 0; i < toprocessed; i++)
            {
                var task = new Task<int>(act);
                tasks.Add(task);
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());
            sw.Stop();
            Debug.WriteLine("TPL\nMax:" + max + " Incomplete:" + counter + " Processed:" + processed + " TimeElapsed:" + sw.Elapsed);


        }

        public int act()
        {
            Interlocked.Increment(ref counter);
            int i = wrk();
            InterlockedMax(ref max, counter);

            Interlocked.Decrement(ref counter);

            Interlocked.Increment(ref processed);
            if (processed == toprocessed) s_are.Set();
            return i;
        }

        public int wrk()
        {
            var tik = Environment.TickCount + 5;
            
            while (Environment.TickCount < tik)
            {
                
            }
            return 0;
        }

        private static Int32 InterlockedMax(ref Int32 target, Int32 val)
        {
            Int32 i, j = target;
            do
            {
                i = j;
                j = Interlocked.CompareExchange(ref target, Math.Max(i, val), i);
            } while (i != j);
            return j;
        }

        [Test]
        [Ignore]
        public void InterlokedMaxTest()
        {
            int v = 0;
            InterlockedMax(ref v, 1);
            Assert.AreEqual(1,v);
            
            InterlockedMax(ref v, 2);
            Assert.AreEqual(2, v);
            
            InterlockedMax(ref v, 1);
            Assert.AreEqual(2, v);
        }
    }
}