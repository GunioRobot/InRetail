using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Joins;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using InRetail.UiCore.Extensions;
using NUnit.Framework;

namespace Tests.InRetail.Exploration
{
    [TestFixture]
    public class RXfixture
    {
        [Test]
        public void ObservableImplementation()
        {
            var observable = new MyObservable();
            
            Action<string> print = (v) => Debug.WriteLine(v);

            IDisposable subscribe1 = observable.Subscribe(print);
            
            

            Thread.Sleep(9000);
            var subject = observable.Record();
            Debug.WriteLine("");
            IDisposable subscribe2 = observable.Subscribe(print);
            IDisposable subscribe3 = observable.Subscribe(print);
            Thread.Sleep(2000);
            subscribe1.Dispose();
            subscribe2.Dispose();
            subscribe3.Dispose();
            Thread.Sleep(1000);


            subject.Subscribe((v) => Debug.WriteLine("Recorded" +v));


            Thread.Sleep(1000);

        }

        [Test]
        public void TaskTest()
        {
            IObservable<Foo> observable = Observable.Create<Foo>((o) =>
                                                                     {
                                                                         Task task = Task.Factory.StartNew(() =>
                                                                                                               {
                                                                                                                   for (
                                                                                                                       int
                                                                                                                           i
                                                                                                                           =
                                                                                                                           0;
                                                                                                                       i <
                                                                                                                       Int32
                                                                                                                           .
                                                                                                                           MaxValue;
                                                                                                                       i
                                                                                                                           ++)
                                                                                                                   {
                                                                                                                       o
                                                                                                                           .
                                                                                                                           OnNext
                                                                                                                           (new Foo
                                                                                                                                {
                                                                                                                                    Id
                                                                                                                                        =
                                                                                                                                        i
                                                                                                                                });
                                                                                                                   }
                                                                                                               });

                                                                         Action unsubscribe = () =>
                                                                                                  {
                                                                                                      Debug.WriteLine(
                                                                                                          "Dispose");
                                                                                                      o.OnCompleted();
                                                                                                      task.Dispose();
                                                                                                  };
                                                                         return unsubscribe;
                                                                     });

            IDisposable disposable = observable.Subscribe((f) => Debug.WriteLine(f.Id));
            Thread.Sleep(2000);
            disposable.Dispose();
            Thread.Sleep(2000);
        }

        [Test]
        public void Test()
        {
            var ints = new[] {1, 2, 3, 4, 5};
            IObservable<int> enumerable = ints.SelectMany(x => x%2 != 0 ? new[] {x} : new int[] {}).ToObservable();

            IObservable<int> observable = ints.ToObservable();
            IDisposable subscribe = observable.Subscribe(x => Debug.WriteLine(x.ToString()));
            IDisposable subscribe2 = enumerable.Subscribe(x => Debug.WriteLine(x.ToString()));
        }

        [Test]
        public void WebRequestTest()
        {
            WebRequest request = WebRequest.Create("http://www.google.ge/");
            IObservable<string> html = from response in request.GetResponseAsync()
                                       let responseStream = response.GetResponseStream()
                                       let reader = new StreamReader(responseStream)
                                       select reader.ReadToEnd();

            IDisposable htmlSub = html.Subscribe(x => Debug.WriteLine(x));

            Thread.Sleep(2000);
        }

        [Test]
        public void ZipTest()
        {
            //Observable.Context = SynchronizationContext.Current;
            var si = new Subject<int>();
            var ss = new Subject<string>();

            IObservable<string> zo = si.Zip(ss, (i, s) => i.ToString() + s);
            string output = "";

            Action<string> actual = (s) => output += s;

            si.Subscribe(x => actual(x.ToString()));
            ss.Subscribe(x => actual(x.ToString()));
            zo.Subscribe(x => actual(x.ToString()));

            si.OnNext(1);
            ss.OnNext("a");
            ss.OnNext("b");
            ss.OnNext("c");
            ss.OnNext("d");
            si.OnNext(2);
            si.OnNext(3);
            si.OnNext(4);
            string expected = "1a1abcd22b33c44d";
            Thread.Sleep(1000);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void AmbTest()
        {
            var enumerable0 = CreateRandomEnumerable("Acho").ToObservable();
            var enumerable1 = CreateRandomEnumerable("foo").ToObservable();
            var enumerable2 = CreateRandomEnumerable("bar").ToObservable();

            var pattern = Observable.Amb(enumerable0, enumerable1, enumerable2 );

            pattern.Subscribe(Console.WriteLine);    
            Thread.Sleep(1000);
            
        }
        
        IEnumerable<T> CreateRandomEnumerable<T>(T value)
        {
            var random = new Random();
            Thread.Sleep(random.Next(1, 10));
            yield return value;
        }
    }

    public class MyObservable : IObservable<String>
    {
        private readonly object _lockobj = new object();
        private readonly List<IObserver<string>> _observers = new List<IObserver<string>>();

        public MyObservable()
        {
            Run();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            lock (_lockobj)
            {
                _observers.Add(observer);
            }

            return new MySubscriptionDisposable(() =>
                                                    {
                                                        lock (_lockobj)
                                                        {
                                                            _observers.Remove(observer);
                                                        }
                                                    });
        }

        private void MyTask()
        {
            for (int i = 1; i < 100; i++)
            {
                _observers.Each(o => o.OnNext(i.ToString()));
                Thread.Sleep(100);
            }
            _observers.Each(o => o.OnCompleted());
        }

        private void Run()
        {
            Task.Factory.StartNew(
                MyTask);
        }

        public class MySubscriptionDisposable : IDisposable
        {
            private readonly Action _action;

            public MySubscriptionDisposable(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action();
            }
        }
    }


    public static class ObservableEx
    {
        public static IObservable<WebResponse> GetResponseAsync(this WebRequest request)
        {
            return Observable.FromAsyncPattern(request.BeginGetResponse, ar => request.EndGetResponse(ar))();
        }
    }

    public class Foo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}