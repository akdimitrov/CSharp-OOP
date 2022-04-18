using System;
using System.Linq;
using NUnit.Framework;

namespace Collections.Tests
{
    public class CircularQueueTests
    {
        [Test]
        public void Test_CircularQueue_ConstructorDefault()
        {
            CircularQueue<int> queue = new CircularQueue<int>();

            Assert.AreEqual(8, queue.Capacity);
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(0, queue.StartIndex);
            Assert.AreEqual(0, queue.EndIndex);
            Assert.AreEqual("[]", queue.ToString());
        }

        [TestCase(8)]
        [TestCase(16)]
        [TestCase(0)]
        public void Test_CircularQueue_ConstructorWithCapacity(int capacity)
        {
            CircularQueue<int> queue = new CircularQueue<int>(capacity);

            Assert.AreEqual(capacity, queue.Capacity);
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(0, queue.StartIndex);
            Assert.AreEqual(0, queue.EndIndex);
            Assert.AreEqual("[]", queue.ToString());
        }

        [Test]
        public void Test_CircularQueue_Enqueue()
        {
            CircularQueue<int> queue = new CircularQueue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(3, queue.EndIndex);
            Assert.AreEqual(0, queue.StartIndex);
            Assert.That(queue.Capacity >= queue.Count);
            Assert.AreEqual("[1, 2, 3]", queue.ToString());
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8, 10)]
        public void Test_CircularQueue_EnqueueWithGrow(int[] itemsToEnqueue, int capacity, int expectedCount)
        {
            CircularQueue<int> queue = new CircularQueue<int>(capacity);
            foreach (var item in itemsToEnqueue)
            {
                queue.Enqueue(item);
            }

            Assert.AreEqual(expectedCount, queue.Count);
            Assert.AreEqual(expectedCount, queue.EndIndex);
            Assert.AreEqual(0, queue.StartIndex);
            Assert.That(queue.Capacity >= queue.Count);
            Assert.AreEqual("[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]", queue.ToString());
        }

        [TestCase(new int[] { 1, 2, 3 }, 2, 1, 2, 3, "[3]")]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8, 0, 0, 0, "[]")]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 5, 3, 5, 0, "[6, 7, 8]")]
        public void Test_CircularQueue_Dequeue(
            int[] itemsToEnque,
            int itemsToDequeue,
            int expectedCount,
            int expectedStartIndex,
            int expectedEndIndex,
            string expectedResult)
        {
            CircularQueue<int> queue = new CircularQueue<int>();
            foreach (var item in itemsToEnque)
            {
                queue.Enqueue(item);
            }

            for (int i = 0; i < itemsToDequeue; i++)
            {
                queue.Dequeue();
            }

            Assert.AreEqual(expectedCount, queue.Count);
            Assert.AreEqual(expectedEndIndex, queue.EndIndex);
            Assert.AreEqual(expectedStartIndex, queue.StartIndex);
            Assert.That(queue.Capacity >= queue.Count);
            Assert.AreEqual(expectedResult, queue.ToString());
        }

        [TestCase(new int[0], 0)]
        [TestCase(new int[] { 1, 2, 3 }, 3)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8)]
        public void Test_CircularQueue_DequeueEmpty(int[] itemsToEnque, int itemsToDequeue)
        {
            CircularQueue<int> queue = new CircularQueue<int>();
            foreach (var item in itemsToEnque)
            {
                queue.Enqueue(item);
            }

            for (int i = 0; i < itemsToDequeue; i++)
            {
                queue.Dequeue();
            }

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Test]
        public void Test_CircularQueue_EnqueueDequeue_WithRangeCross()
        {
            CircularQueue<int> queue = new CircularQueue<int>(5);
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            int firstItem = queue.Dequeue();
            Assert.AreEqual(10, firstItem);

            int secondItem = queue.Dequeue();
            Assert.AreEqual(20, secondItem);

            queue.Enqueue(40);
            queue.Enqueue(50);
            queue.Enqueue(60);
            Assert.AreEqual("[30, 40, 50, 60]", queue.ToString());
            Assert.AreEqual(4, queue.Count);
            Assert.AreEqual(5, queue.Capacity);
            Assert.That(queue.StartIndex > queue.EndIndex);
        }

        [TestCase(new int[] { 1, 2, 3 }, 2, 3)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 0, 1)]
        public void Test_CircularQueue_Peek(int[] itemsToEnque, int itemsToDequeue, int expectedItem)
        {
            CircularQueue<int> queue = new CircularQueue<int>();
            foreach (var item in itemsToEnque)
            {
                queue.Enqueue(item);
            }

            for (int i = 0; i < itemsToDequeue; i++)
            {
                queue.Dequeue();
            }

            Assert.AreEqual(expectedItem, queue.Peek());
        }

        [TestCase(new int[0], 0)]
        [TestCase(new int[] { 1, 2, 3 }, 3)]
        public void Test_CircularQueue_PeekEmpty(int[] itemsToEnque, int itemsToDequeue)
        {
            CircularQueue<int> queue = new CircularQueue<int>();
            foreach (var item in itemsToEnque)
            {
                queue.Enqueue(item);
            }

            for (int i = 0; i < itemsToDequeue; i++)
            {
                queue.Dequeue();
            }

            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Test]
        public void Test_CircularQueue_ToArray()
        {
            CircularQueue<int> queue = new CircularQueue<int>();
            for (int i = 1; i <= 8; i++)
            {
                queue.Enqueue(i);
            }

            int[] queueArray = queue.ToArray();
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, queueArray);
        }

        [Test]
        public void Test_CircularQueue_ToArrayWithRangeCross()
        {
            CircularQueue<int> queue = new CircularQueue<int>(5);
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            queue.Dequeue();
            queue.Dequeue();

            queue.Enqueue(40);
            queue.Enqueue(50);
            queue.Enqueue(60);

            int[] queueArray = queue.ToArray();
            Assert.AreEqual(new int[] { 30, 40, 50, 60 }, queueArray);
        }

        [Test]
        public void Test_CircularQueue_MultipleOperations()
        {
            const int InitialCapacity = 2;
            const int IterationsCount = 300;
            CircularQueue<int> queue = new CircularQueue<int>(InitialCapacity);
            int addedCount = 0;
            int removedCount = 0;
            int counter = 0;
            for (int i = 0; i < IterationsCount; i++)
            {
                Assert.That(queue.Count == addedCount - removedCount);

                queue.Enqueue(++counter);
                addedCount++;
                Assert.That(queue.Count == addedCount - removedCount);

                queue.Enqueue(++counter);
                addedCount++;
                Assert.That(queue.Count == addedCount - removedCount);

                int firstElement = queue.Peek();
                Assert.That(firstElement == removedCount + 1);

                int removedElement = queue.Dequeue();
                removedCount++;
                Assert.That(removedElement == removedCount);
                Assert.That(queue.Count == addedCount - removedCount);

                int[] expectedElements = Enumerable.Range(removedCount + 1, addedCount - removedCount).ToArray();
                string expectedString = $"[{string.Join(", ", expectedElements)}]";

                int[] queueAsArray = queue.ToArray();
                string queueAsString = queue.ToString();

                CollectionAssert.AreEqual(expectedElements, queueAsArray);
                Assert.AreEqual(expectedString, queueAsString);
                Assert.That(queue.Capacity >= InitialCapacity);
            }

            Assert.That(queue.Capacity > InitialCapacity);
        }

        [Test]
        [Timeout(500)]
        public void Test_CircularQueue_1MullionItems()
        {
            const int IterationsCount = 1_000_000;
            CircularQueue<int> queue = new CircularQueue<int>();
            int addedCount = 0;
            int removedCount = 0;
            int counter = 0;
            for (int i = 0; i < IterationsCount / 2; i++)
            {
                queue.Enqueue(++counter);
                addedCount++;
                queue.Enqueue(++counter);
                addedCount++;
                queue.Dequeue();
                removedCount++;
            }

            Assert.That(queue.Count == addedCount - removedCount);
        }
    }
}