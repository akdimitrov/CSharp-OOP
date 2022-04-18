using System;
using System.Linq;
using NUnit.Framework;

namespace Collections.Tests
{
    class CollectionTests
    {
        [TestCase(new int[] { }, 0, 16, "[]")]
        [TestCase(new int[] { 1, 2, 3 }, 3, 16, "[1, 2, 3]")]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10, 20, "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]")]
        public void Test_Collection_Constructor_ShouldPass_WithValidData(
            int[] items, int expectedCount, int expectedCapacity, string expectedStringResult)
        {
            Collection<int> collection = new Collection<int>(items);

            Assert.AreEqual(expectedCapacity, collection.Capacity);
            Assert.AreEqual(expectedCount, collection.Count);
            Assert.AreEqual(expectedStringResult, collection.ToString());
        }

        [TestCase(new int[] { 1 }, new int[] { 2 }, 2, 16, "[1, 2]")]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                  new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }, 17, 32,
                  "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17]")]
        public void Test_Collection_Add(
            int[] ctorItems, int[] itemsToAdd, int expectedCount, int expectedCapacity, string expectedStringResult)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            foreach (var item in itemsToAdd)
            {
                collection.Add(item);
            }

            Assert.AreEqual(expectedCapacity, collection.Capacity);
            Assert.AreEqual(expectedCount, collection.Count);
            Assert.AreEqual(expectedStringResult, collection.ToString());
        }

        [TestCase(new int[] { 1 }, new int[] { 2 }, 2, 16, "[1, 2]")]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                  new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }, 17, 32,
                  "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17]")]
        public void Test_Collection_AddRange(
            int[] ctorItems, int[] itemsToAdd, int expectedCount, int expectedCapacity, string expectedStringResult)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            collection.AddRange(itemsToAdd);

            Assert.AreEqual(expectedCapacity, collection.Capacity);
            Assert.AreEqual(expectedCount, collection.Count);
            Assert.AreEqual(expectedStringResult, collection.ToString());
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            Collection<int> collection = new Collection<int>(1, 2, 3, 4, 5, 6);
            Assert.AreEqual(1, collection[0]);
            Assert.AreEqual(3, collection[2]);
            Assert.AreEqual(6, collection[5]);
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            Collection<int> collection = new Collection<int>(1, 2, 3, 4, 5, 6);
            int item;
            Assert.Throws<ArgumentOutOfRangeException>(() => item = collection[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => item = collection[6]);
        }

        [TestCase(new int[] { }, 0, 1, 1, "[1]")]
        [TestCase(new int[] { 20, 30, 50 }, 2, 40, 4, "[20, 30, 40, 50]")]
        public void Test_Collection_InsertAt_ValidIndex(
            int[] ctorItems, int index, int item, int expectedCount, string expectedResult)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            collection.InsertAt(index, item);

            Assert.AreEqual(expectedCount, collection.Count);
            Assert.AreEqual(item, collection[index]);
            Assert.AreEqual(expectedResult, collection.ToString());
        }

        [TestCase(new int[] { }, -1, 5)]
        [TestCase(new int[] { }, 1, 5)]
        [TestCase(new int[] { 15, 0, 35, 98, 52, 48, 9999 }, -1, 5)]
        [TestCase(new int[] { 1, 2, 3 }, 4, 4)]
        public void Test_Collection_InsertAt_InvalidIndex(int[] ctroItems, int index, int item)
        {
            Collection<int> collection = new Collection<int>(ctroItems);
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.InsertAt(index, item));
        }

        [TestCase(new int[] { 22, 33, 44, 55, 66 }, 1, 3, "[22, 55, 44, 33, 66]")]
        [TestCase(new int[] { 5, 2, 3, 4, 1 }, 0, 4, "[1, 2, 3, 4, 5]")]
        [TestCase(new int[] { 1, 2 }, 0, 1, "[2, 1]")]
        public void Test_Collection_Exchnage_ValidData(int[] ctorItems, int index1, int index2, string expectedResult)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            int item1 = collection[index1];
            int item2 = collection[index2];
            collection.Exchange(index1, index2);

            Assert.AreEqual(item2, collection[index1]);
            Assert.AreEqual(item1, collection[index2]);
            Assert.AreEqual(expectedResult, collection.ToString());
        }

        [TestCase(new int[] { 22, 33, 44, 55, 66 }, -1, 3)]
        [TestCase(new int[] { 5, 2, 3, 4, 1 }, 0, 5)]
        [TestCase(new int[] { 1, 2 }, int.MinValue, 1)]
        [TestCase(new int[] { 1, 2 }, 0, int.MaxValue)]
        public void Test_Collection_Exchnage_InvalidData(int[] ctorItems, int index1, int index2)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.Exchange(index1, index2));
        }

        [TestCase(new int[] { 1 }, 0, 1, 0, "[]")]
        [TestCase(new int[] { 20, 30, 50 }, 1, 30, 2, "[20, 50]")]
        [TestCase(new int[] { 20, 30, 50 }, 2, 50, 2, "[20, 30]")]
        public void Test_Collection_RemoveAt_ValidIndex(
            int[] ctorItems, int index, int itemToRemove, int expectedCount, string expectedResult)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            int removedItem = collection.RemoveAt(index);

            Assert.AreEqual(expectedCount, collection.Count);
            Assert.AreEqual(itemToRemove, removedItem);
            Assert.AreEqual(expectedResult, collection.ToString());
        }

        [TestCase(new int[] { }, -1)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { }, 1)]
        [TestCase(new int[] { 15, 0, 35, 98, 52, 48, 9999 }, -1)]
        [TestCase(new int[] { 1, 2, 3 }, 4)]
        public void Test_Collection_RemoveAt_InvalidIndex(int[] ctroItems, int index)
        {
            Collection<int> collection = new Collection<int>(ctroItems);
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.RemoveAt(index));
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        public void Test_Collection_Clear(int[] ctorItems)
        {
            Collection<int> collection = new Collection<int>(ctorItems);
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
            Assert.AreEqual("[]", collection.ToString());
        }

        [Test]
        public void Test_Collection_MultipleOperations()
        {
            const int IterationsCount = 300;
            Collection<int> collection = new Collection<int>();
            int addedCount = 0;
            int counter = 0;
            for (int i = 0; i < IterationsCount; i++)
            {
                collection.AddRange(Enumerable.Range(1, 999).ToArray());
                for (int j = 0; j < 999; j++)
                {
                    collection.RemoveAt(collection.Count - 1);
                }

                Assert.That(collection.Count == addedCount);

                collection.Add(++counter);
                addedCount++;
                Assert.That(collection.Count == addedCount);

                collection.Add(++counter);
                addedCount++;
                Assert.That(collection.Count == addedCount);

                collection.InsertAt(1, counter);
                addedCount++;
                Assert.That(collection.Count == addedCount);

                int lastElement = collection[counter];
                Assert.That(lastElement == counter);

                int removedElement = collection.RemoveAt(1);
                addedCount--;
                Assert.That(removedElement == counter);
                Assert.That(collection.Count == addedCount);

                for (int k = 0; k < 2; k++)
                {
                    int element1 = collection[0];
                    int element2 = collection[1];
                    collection.Exchange(0, 1);
                    Assert.That(collection[1] == element1);
                    Assert.That(collection[0] == element2);
                }

                int[] expectedElements = Enumerable.Range(1, addedCount).ToArray();
                string expectedString = $"[{string.Join(", ", expectedElements)}]";
                Assert.AreEqual(expectedString, collection.ToString());
            }
        }

        [Test]
        [Timeout(500)]
        public void Test_Collection_1MillionItems()
        {
            const int IterationsCount = 1_000_000;
            Collection<int> collection = new Collection<int>();
            int addedCount = 0;
            int removedCount = 0;
            int counter = 0;
            for (int i = 0; i < IterationsCount / 2; i++)
            {
                collection.Add(++counter);
                addedCount++;
                collection.Add(++counter);
                addedCount++;
                collection.RemoveAt(removedCount++);
            }

            Assert.That(collection.Count == addedCount - removedCount);
        }

    }
}
