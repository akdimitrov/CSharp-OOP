namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        [Test]
        public void ConstructorShouldInitialize_WihtValidData()
        {
            Book book = new Book("TheBook", "X");

            Assert.AreEqual("TheBook", book.BookName);
            Assert.AreEqual("X", book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }

        [Test]
        public void ConstructorShouldThrowException_WihtInvalidBookName()
        {
            Assert.Throws<ArgumentException>(() => new Book(null, "X"));
            Assert.Throws<ArgumentException>(() => new Book("", "X"));
        }

        [Test]
        public void ConstructorShouldThrowException_WihtInvalidAuthor()
        {
            Assert.Throws<ArgumentException>(() => new Book("Sahara", null));
            Assert.Throws<ArgumentException>(() => new Book("Jungle", ""));
        }

        [Test]
        public void AddFootnote_PositiveTest()
        {
            Book book = new Book("TheBook", "X");
            book.AddFootnote(1, "Test");

            Assert.AreEqual(1, book.FootnoteCount);
        }

        [Test]
        public void AddFootnote_ShouldThrowException_If_FootNoteNumnberExist()
        {
            Book book = new Book("TheBook", "X");
            book.AddFootnote(1, "Test");

            Assert.Throws<InvalidOperationException>(() => book.AddFootnote(1, "Test2"));
        }

        [Test]
        public void FindFootnote_PositiveTest()
        {
            Book book = new Book("TheBook", "X");
            book.AddFootnote(1, "Test");
            book.AddFootnote(2, "Hi");
            book.AddFootnote(3, "Interesting");

            string expectedResult = "Footnote #2: Hi";
            Assert.AreEqual(expectedResult, book.FindFootnote(2));
        }

        [Test]
        public void FindFootnote_ShouldThrowException_If_FootNoteNumnberDoesNotExist()
        {
            Book book = new Book("TheBook", "X");
            book.AddFootnote(1, "Test");
            book.AddFootnote(2, "Hi");
            book.AddFootnote(3, "Interesting");

            Assert.Throws<InvalidOperationException>(() => book.FindFootnote(4));
        }

        [Test]
        public void AlterFootnote_PositiveTest()
        {
            Book book = new Book("TheBook", "X");
            book.AddFootnote(1, "Test");
            book.AddFootnote(2, "Hi");
            book.AddFootnote(3, "Interesting");

            book.AlterFootnote(2, "Bye");
            string expectedResult = "Footnote #2: Bye";
            Assert.AreEqual(expectedResult, book.FindFootnote(2));
        }

        [Test]
        public void AlterFootnote_ShouldThrowException_If_FootNoteNumnberDoesNotExist()
        {
            Book book = new Book("TheBook", "X");
            book.AddFootnote(1, "Test");
            book.AddFootnote(2, "Hi");
            book.AddFootnote(3, "Interesting");

            Assert.Throws<InvalidOperationException>(() => book.AlterFootnote(4, "Some text"));
        }
    }
}