namespace Database.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(new int[] { -1, -54, -64 })]
        [TestCase(new int[] { int.MaxValue, int.MinValue, 0 })]
        [TestCase(new int[0])]
        public void Construnctor_WihtValidData_ShouldPass(int[] paramneters)
        {
            Database database = new Database(paramneters);
            Assert.AreEqual(paramneters.Length, database.Count);
        }

        [TestCase(new int[] { 1, 3 }, new int[] { 43, 53, 5 }, 5)]
        [TestCase(new int[0], new int[] { int.MinValue, int.MaxValue, 756765 }, 3)]
        [TestCase(new int[0], new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 16)]
        public void Add_WithValidData_PositiveTest(int[] ctorParams, int[] elementsToAdd, int expectedCount)
        {
            Database database = new Database(ctorParams);
            foreach (var element in elementsToAdd)
            {
                database.Add(element);
            }

            Assert.AreEqual(expectedCount, database.Count);
        }

        [TestCase(new int[0], new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 1)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new int[] { 9, 10, 11, 12, 13, 14, 15, 16 }, 234)]
        public void Add_WithInvalidData_NegativeTest(int[] ctorParams, int[] elementsToAdd, int errorParam)
        {
            Database database = new Database(ctorParams);
            foreach (var element in elementsToAdd)
            {
                database.Add(element);
            }

            Assert.Throws<InvalidOperationException>(() => database.Add(errorParam));
        }

        [TestCase(new int[] { 1, 3 }, new int[] { 43, 53, 5 }, 5, 0)]
        [TestCase(new int[0], new int[] { int.MinValue, int.MaxValue, 756765 }, 2, 1)]
        [TestCase(new int[0], new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 8, 8)]
        public void Remove_WithValidData_PositiveTest(int[] ctorParams, int[] elementsToAdd, int elementsToRemove, int expectedCount)
        {
            Database database = new Database(ctorParams);
            foreach (var element in elementsToAdd)
            {
                database.Add(element);
            }

            for (int i = 0; i < elementsToRemove; i++)
            {
                database.Remove();
            }

            Assert.AreEqual(expectedCount, database.Count);
        }

        [TestCase(new int[] { 1, 3 }, 2)]
        [TestCase(new int[0], 0)]
        public void Remove_WithInvalidData_NegativeTest(int[] ctorParams, int elementsToRemove)
        {
            Database database = new Database(ctorParams);
            for (int i = 0; i < elementsToRemove; i++)
            {
                database.Remove();
            }

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [TestCase(new int[0], new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void Fetch_ShouldReturnDatabaseArrayCopy(int[] ctorParams, int[] expectedResult)
        {
            Database database = new Database(ctorParams);
            CollectionAssert.AreEqual(expectedResult, database.Fetch());
        }
    }
}
