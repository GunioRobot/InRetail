using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading;
namespace Exploration.Tests.Exploration
{
    [TestFixture]
    public class EnumerableFixture
    {
        private List<object> _sideEffect;

        [SetUp]
        public void SetUp()
        {
            _sideEffect = new List<object>();
        }

        [Test]
        public void SideEffectWithRunOperator()
        {
            SideEffect().Run();

            Assert.AreEqual("Test 1", _sideEffect[0]);
            Assert.AreEqual("Test 2", _sideEffect[1]);
        }

        [Test]
        public void SideEffectWithRunOperatorWithAction()
        {
            var values = new List<int>();
            SideEffect().Run(values.Add);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(2, values[1]);
        }

        [Test]
        public void NoSideEffectWithDoOperator()
        {
            var values = new List<int>();
            SideEffect().Do(values.Add);

            Assert.AreEqual(0, values.Count);

        }

        [Test]
        public void SideEffectWithDoOperatorWhenRunUsed()
        {
            var values = new List<int>();
            SideEffect().Do(values.Add).Run();

            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(2, values[1]);

        }

        [Test]
        public void PrimitiveOperatorReturn()
        {
            var values = new List<int>();
            EnumerableEx.Return(11).Run(values.Add);

            Assert.AreEqual(11, values[0]);
        }

        [Test]
        public void PrimitiveOperatorStartWith()
        {
            var values = new List<int>();
            EnumerableEx.Return(11).StartWith(1).Run(values.Add);

            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(11, values[1]);
        }
        [Test]
        public void TTT()
        {
            SideEffect().Run(Console.WriteLine);
        }

        [Test]
        public void MergeTest()
        {
            SideEffect2(1, 2).Merge(SideEffect2(5, 10)).Merge(SideEffect2(20, 30)).Run(Console.WriteLine);
        }

        public IEnumerable<int> SideEffect()
        {
            _sideEffect.Add("Test 1");
            yield return 1;
            _sideEffect.Add("Test 2");
            yield return 2;
            yield break;
        }

        public static IEnumerable<int> SideEffect2(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                Thread.Sleep(i * 10);
                Console.Write("\t" + Thread.CurrentThread.ManagedThreadId + "\t");
                yield return i;
            }
            yield break;
        }

    }
}