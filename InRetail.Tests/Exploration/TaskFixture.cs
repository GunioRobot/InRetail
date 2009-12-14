using System;
using System.Threading;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class TaskFixture
    {

        [Test]
        public void ProcessTreeByThreadPool()
        {
            var tree = CreateTree(5, 1);
            WalkTreeUsingThreadPool(tree, i => Debug.WriteLine(i));
        }

        [Test]
        public void ProcessTreeByTask()
        {
        

            var tree = CreateTree(18, 1);
            WalkTreeUsingTask(tree, i => Debug.WriteLine(i));
            
           
            
        }
        [Test]
        public void CancelingTask()
        {
            CancellationTokenSource cst = new CancellationTokenSource();
            CancellationToken ct = cst.Token;

            Task.Factory.StartNew(delegate
                                      {
                                          for (int i = 0; i < 100; i++)
                                          {
                                              Thread.Sleep(20);
                                              Debug.WriteLine(i);
                                          }
                                          
                                      },
                                      ct);
            Thread.Sleep(200);
            cst.Cancel();
        }

        static void WalkTreeUsingTask<T>(Tree<T> root, Action<T> action)
        {
            if (root == null) return;
            Task left = Task.Factory.StartNew(() => WalkTreeUsingTask(root.Left, action));
            Task right = Task.Factory.StartNew(() => WalkTreeUsingTask(root.Right, action));
            action(root.Data);
            Task.WaitAll(new []{left, right});
        }
        static void WalkTreeUsingThreadPool<T>(Tree<T> root, Action<T> action)
        {
            if (root == null) return;
            using (CountdownEvent ce = new CountdownEvent(2))
            {
                ThreadPool.QueueUserWorkItem(delegate
                                                 {
                                                     WalkTreeUsingThreadPool(root.Left, action);
                                                     ce.Signal();
                                                 });
                ThreadPool.QueueUserWorkItem(delegate
                                                 {
                                                     WalkTreeUsingThreadPool(root.Right, action);
                                                     ce.Signal();
                                                 });
                action(root.Data);
                ce.Wait();
            }

        }

        static Tree<int> CreateTree(int depth, int rootData)
        {
            if (--depth == 0) return null;

            var createTree = new Tree<int>() { Data = rootData };
            createTree.Left = CreateTree(depth, ++rootData);
            createTree.Right = CreateTree(depth, ++rootData);
            return createTree;
        }
    }
    class Tree<T>
    {
        public Tree<T> Left, Right;
        public T Data;


    }
}
