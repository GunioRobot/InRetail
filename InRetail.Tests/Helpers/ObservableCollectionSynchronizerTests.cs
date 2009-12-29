using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

using InRetail.UiCore.Helpers;

namespace Tests.InRetail.Helpers
{
    public class ObservableCollectionSynchronizerTests
    {
        private readonly ObservableCollection<int> collection1;
        private readonly ObservableCollection<string> collection2;
        private ObservableCollectionSynchronizer<int, string> _synchronizer;

        public ObservableCollectionSynchronizerTests()
        {
            collection1 = new ObservableCollection<int>(new[] {1, 2, 3});
            collection2 = new ObservableCollection<string>();
            _synchronizer = new ObservableCollectionSynchronizer<int, string>(collection1, collection2,
                                                                              (x) => x.ToString());
        }

        [Fact]
        public void Should_In_Sync_After_Add_One_Item()
        {
            collection1.Add(1);

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_In_Sync_After_Insert_One_Item()
        {
            collection1.Insert(0, 1);
            collection1.Insert(0, 0);

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_In_Sync_After_Remove_One_Item()
        {
            collection1.Remove(2);

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_In_Sync_After_RemoveAt_One_Item()
        {
            collection1.RemoveAt(1);

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_In_Sync_After_Move_One_Item()
        {
            collection1.Move(0, 2);

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_In_Sync_After_Clear()
        {
            collection1.Clear();

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_In_Sync_On_ChangeItem()
        {
            collection1[2] = 11;

            AssertCollectionItemsIsEqual(collection1, collection2, (t, u) => t.ToString() == u);
        }

        [Fact]
        public void Should_Throw_If_Sync_Target_Collection_Is_Already_Populated()
        {
            var collection = new ObservableCollection<int>(new[] {1, 2, 3});
            var ivalidArgument = new ObservableCollection<string>(new[] {"some item"});
            Assert.Throws<ArgumentException>(
                () => new ObservableCollectionSynchronizer<int, string>(collection, ivalidArgument, (x) => x.ToString()));
        }

        [Fact]
        public void Should_Throw_If_syncSourceCollection_not_implement_INotifyCollectionChanged()
        {
            var ivalidArgument = new List<int>(new[] {1, 2, 3});
            var coll2 = new ObservableCollection<string>();
            Assert.Throws<ArgumentException>(
                () => new ObservableCollectionSynchronizer<int, string>(ivalidArgument, coll2, (x) => x.ToString()));
        }

        [Fact]
        public void Should_Be_Disposable()
        {
            var reference = new WeakReference(_synchronizer);

            Assert.True(reference.IsAlive);

            _synchronizer.Dispose();
            _synchronizer = null;

            GC.Collect();

            Assert.False(reference.IsAlive);
        }

        private static void AssertCollectionItemsIsEqual<T, U>(IList<T> collection1, IList<U> collection2,
                                                               Func<T, U, bool> pred)
        {
            Assert.Equal(collection1.Count, collection2.Count);
            for (int i = 0; i < collection1.Count; i++)
                Assert.True(pred(collection1[i], collection2[i]));
        }
    }
}