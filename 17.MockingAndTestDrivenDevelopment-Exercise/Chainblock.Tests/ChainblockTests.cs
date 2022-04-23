using System;
using System.Collections.Generic;
using System.Linq;
using Chainblock.Contracts;
using NUnit.Framework;

namespace Chainblock.Tests
{
    [TestFixture]
    public class ChainblockTests
    {
        private IChainblock chainblock;

        [SetUp]
        public void SetUp()
        {
            this.chainblock = new Chainblock();
        }

        [Test]
        public void AddMethod_ShouldAddTransaction_IfDataIsValid()
        {
            ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            this.chainblock.Add(transaction);
            this.chainblock.Add(transaction);

            Assert.AreEqual(1, this.chainblock.Count);
            Assert.True(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsMethod_ShouldReturnTrue_IfDataExist()
        {
            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Successfull, "Gregg", "Correy", 250);
            this.chainblock.Add(transaction1);

            Assert.True(this.chainblock.Contains(transaction1));
            Assert.True(this.chainblock.Contains(transaction1.Id));
            Assert.False(this.chainblock.Contains(transaction2));
            Assert.False(this.chainblock.Contains(transaction2.Id));
        }

        [Test]
        public void ChangeTransactionStatus_ShouldChangeStatus_IfDataExist()
        {
            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Successfull, "Gregg", "Correy", 250);
            this.chainblock.Add(transaction1);
            this.chainblock.Add(transaction2);

            this.chainblock.ChangeTransactionStatus(1, TransactionStatus.Failed);
            var chainblockTransaction = this.chainblock.GetById(1);
            Assert.AreEqual(TransactionStatus.Failed, chainblockTransaction.Status);
        }

        [Test]
        public void ChangeTransactionStatus_ShouldThrowException_IfDataDoesNotExist()
        {
            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Successfull, "Gregg", "Correy", 250);
            this.chainblock.Add(transaction1);
            this.chainblock.Add(transaction2);
            this.chainblock.RemoveTransactionById(transaction2.Id);

            Assert.Throws<ArgumentException>(() => this.chainblock.ChangeTransactionStatus(2, TransactionStatus.Failed));
        }

        [Test]

        public void RemoveTransactionById_ShouldRemoveTransaction_IfExist()
        {
            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Successfull, "Gregg", "Correy", 250);
            this.chainblock.Add(transaction1);
            this.chainblock.Add(transaction2);
            this.chainblock.RemoveTransactionById(transaction2.Id);

            Assert.AreEqual(1, this.chainblock.Count);
            Assert.False(this.chainblock.Contains(transaction2));
        }

        [Test]
        public void RemoveTransactionById_ShouldThrowException_IfIdDoesNotExist()
        {
            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Successfull, "Gregg", "Correy", 250);
            this.chainblock.Add(transaction1);
            this.chainblock.Add(transaction2);
            this.chainblock.RemoveTransactionById(transaction2.Id);

            Assert.Throws<InvalidOperationException>(() => this.chainblock.RemoveTransactionById(transaction2.Id));
        }

        [Test]
        public void GetById_ShouldReturnTransaction_IfIdExist()
        {
            ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transactionCopy = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            this.chainblock.Add(transaction);

            var actualTransaction = this.chainblock.GetById(transaction.Id);
            Assert.AreEqual(transaction, actualTransaction);
            Assert.AreEqual(transaction, transactionCopy);
            Assert.True(actualTransaction.Id == 1);
        }

        [Test]
        public void GetById_ShouldThrowException_IfIdDoesNotExist()
        {
            ITransaction transaction1 = new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25);
            ITransaction transaction2 = new Transaction(2, TransactionStatus.Successfull, "Gregg", "Correy", 250);
            this.chainblock.Add(transaction1);
            this.chainblock.Add(transaction2);
            this.chainblock.RemoveTransactionById(transaction2.Id);

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetById(transaction2.Id));
        }

        [Test]
        public void GetByTransactionStatus_ShouldReturnTransactions_InDescendingOrderByAmount_IfAny()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "John", "Michael", 81.33)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull).ToArray();
            var expectedTransactions = new ITransaction[] { transactions[2], transactions[0], transactions[4] };
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        [Test]
        public void GetByTransactionStatus_ShouldThrowException_IfNone()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetByTransactionStatus(TransactionStatus.Failed));
        }

        [Test]
        public void GetAllSendersWithTransactionStatus_ShouldReturnAllSendersAndTheirTransactions_OrderedByTransactionAmount_IfAny()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Michael", 81.33),
                new Transaction(5, TransactionStatus.Failed, "Max", "Jom", 48)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Successfull).ToArray();
            var expectedTransactions = new string[] { transactions[2].From, transactions[0].From, transactions[4].From };
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        [Test]
        public void GetAllSendersWithTransactionStatus_ShouldThrowException_IfNone()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Michael", 81.33)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Failed));
        }

        [Test]
        public void GetAllReceiversWithTransactionStatus_ShouldReturnAllReceiversAndTheirTransactions_OrderedByTransactionAmount_IfAny()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 81.33),
                new Transaction(6, TransactionStatus.Failed, "Max", "Jom", 48)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Successfull).ToArray();
            var expectedTransactions = new string[] { transactions[2].To, transactions[0].To, transactions[4].To };
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        [Test]
        public void GetAllReceiversWithTransactionStatus_ShouldThrowException_IfNone()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Michael", 81.33)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Failed));
        }

        [Test]
        public void GetAllOrderedByAmountDescendingThenById_ShouldReturnAllTransactions()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(6, TransactionStatus.Failed, "Max", "Jom", 48)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetAllOrderedByAmountDescendingThenById().ToArray();
            var expectedTransactions = new ITransaction[] {
                transactions[2], transactions[1], transactions[0], transactions[4], transactions[5], transactions[3] };
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        [Test]
        public void GetBySenderOrderedByAmountDescending_ShouldReturnAllTransactionsBySender_IfAny()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Jom", 48)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetBySenderOrderedByAmountDescending("Paul").ToArray();
            var expectedTransactions = new ITransaction[] { transactions[4], transactions[2] };
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        [Test]
        public void GetBySenderOrderedByAmountDescending_ShouldThrowException_IfNone()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Jom", 48)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetBySenderOrderedByAmountDescending("Simeon"));
        }

        [Test]
        public void GetByReceiverOrderedByAmountThenById_ShouldReturnTransactionsByReceiver_OrderedByAmountDescending_ThenById_IfAny()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetByReceiverOrderedByAmountThenById("Stan").ToArray();
            var expectedTransactions = new ITransaction[] { transactions[4], transactions[2], transactions[5] };
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        [Test]
        public void GetByReceiverOrderedByAmountThenById_ShouldThrowException_IfNone()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 150.25),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetByReceiverOrderedByAmountThenById("Simeon"));
        }

        [TestCaseSource("TestCases_GetByTransactionStatusAndMaximumAmount")]
        public void GetByTransactionStatusAndMaximumAmount_ShouldReturnTransactions_OrderedByAmountDescending_OrEmptyCollection_IfNone(
            ITransaction[] transactions,
            TransactionStatus status,
            double maxAmount,
            ITransaction[] expectedTransactions)
        {
            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetByTransactionStatusAndMaximumAmount(status, maxAmount).ToArray();
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        public static IEnumerable<TestCaseData> TestCases_GetByTransactionStatusAndMaximumAmount()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 84),
                new Transaction(2, TransactionStatus.Aborted, "Gregg", "Correy", 250),
                new Transaction(3, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(transactions, TransactionStatus.Successfull, 151,
                    new ITransaction[]{transactions[2],transactions[0]}),

                new TestCaseData(transactions, TransactionStatus.Successfull, 150.25,
                    new ITransaction[]{transactions[2],transactions[0]}),

                new TestCaseData(transactions, TransactionStatus.Successfull, 150,
                    new ITransaction[]{transactions[0]}),

                new TestCaseData(transactions, TransactionStatus.Successfull, 340,
                    new ITransaction[]{transactions[4],transactions[2],transactions[0]}),

                new TestCaseData(transactions, TransactionStatus.Successfull, 83.99,
                    new ITransaction[0])
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        [TestCaseSource("TestCases_GetBySenderAndMinimumAmountDescending")]
        public void GetBySenderAndMinimumAmountDescending_ShouldReturnTransactions_OrderedByAmountDescending_IfAny(
            ITransaction[] transactions,
            string sender,
            double minAmount,
            ITransaction[] expectedTransactions)
        {
            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetBySenderAndMinimumAmountDescending(sender, minAmount).ToArray();
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        public static IEnumerable<TestCaseData> TestCases_GetBySenderAndMinimumAmountDescending()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 84),
                new Transaction(2, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(3, TransactionStatus.Aborted, "Paul", "Correy", 250),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(transactions, "Paul", 150,
                    new ITransaction[]{transactions[4],transactions[2],transactions[1]}),

                new TestCaseData(transactions, "Paul", 150.25,
                    new ITransaction[]{transactions[4],transactions[2]}),

                new TestCaseData(transactions, "Sam", 0.24,
                    new ITransaction[]{transactions[3]}),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        [TestCase("Glen", 150)]
        [TestCase("Paul", 340)]
        [TestCase("Sam", 9000)]
        [TestCase("", 100)]
        public void GetBySenderAndMinimumAmountDescending_ShouldThrowException_IfNone(string sender, double amount)
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "George", 84),
                new Transaction(2, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(3, TransactionStatus.Aborted, "Paul", "Correy", 250),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetBySenderAndMinimumAmountDescending(sender, amount));
        }

        [TestCaseSource("TestCases_GetByReceiverAndAmountRange")]
        public void GetByReceiverAndAmountRange_ShouldReturnTransactions_OrderedByAmountDescending_ThenById_IfAny(
            ITransaction[] transactions,
            string receiver,
            double lo,
            double hi,
            ITransaction[] expectedTransactions)
        {
            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetByReceiverAndAmountRange(receiver, lo, hi).ToArray();
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        public static IEnumerable<TestCaseData> TestCases_GetByReceiverAndAmountRange()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "Correy", 84),
                new Transaction(2, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(3, TransactionStatus.Aborted, "Paul", "Correy", 250),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(transactions, "Stan", 150.25, 340,
                    new ITransaction[]{transactions[1],transactions[5]}),

                new TestCaseData(transactions, "Stan", 0, 341,
                    new ITransaction[]{transactions[4],transactions[1],transactions[5]}),

                new TestCaseData(transactions, "Stan", 150.26, 340.01,
                    new ITransaction[]{transactions[4]}),

                new TestCaseData(transactions, "Correy", 250, 250.01,
                    new ITransaction[]{transactions[2]}),

                new TestCaseData(transactions, "Elvis", 0, 1000,
                    new ITransaction[]{transactions[3]}),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        [TestCase("Glen", 150, 340)]
        [TestCase("Paul", 0, 300)]
        [TestCase("Stan", 340.01, 500)]
        [TestCase("Stan", 0, 150.25)]
        [TestCase("Stan", 151, 340)]
        [TestCase("", 1, 999)]
        public void GetByReceiverAndAmountRange_ShouldThrowException_IfNone(string receiver, double lo, double hi)
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "Correy", 84),
                new Transaction(2, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(3, TransactionStatus.Aborted, "Paul", "Correy", 250),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            Assert.Throws<InvalidOperationException>(() => this.chainblock.GetByReceiverAndAmountRange(receiver, lo, hi));
        }

        [TestCaseSource("TestCases_GetAllInAmountRange")]
        public void GetAllInAmountRange_ShouldReturnTransactions_OrderedByAmountDescending_OrEmptyCollection_IfNone(
            ITransaction[] transactions,
            double lo,
            double hi,
            ITransaction[] expectedTransactions)
        {
            foreach (var transaction in transactions)
            {
                this.chainblock.Add(transaction);
            }

            var result = this.chainblock.GetAllInAmountRange(lo, hi).ToArray();
            CollectionAssert.AreEqual(expectedTransactions, result);
        }

        public static IEnumerable<TestCaseData> TestCases_GetAllInAmountRange()
        {
            ITransaction[] transactions = new ITransaction[]
            {
                new Transaction(1, TransactionStatus.Successfull, "Ivan", "Correy", 84),
                new Transaction(2, TransactionStatus.Successfull, "Paul", "Stan", 150.25),
                new Transaction(3, TransactionStatus.Aborted, "Paul", "Correy", 250),
                new Transaction(4, TransactionStatus.Unauthorized, "Sam", "Elvis", 0.25),
                new Transaction(5, TransactionStatus.Successfull, "Paul", "Stan", 340),
                new Transaction(6, TransactionStatus.Failed, "Max", "Stan", 150.25)
            };

            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(transactions, 0.25, 340, transactions),

                new TestCaseData(transactions, 0, 1000, transactions),

                new TestCaseData(transactions, double.MinValue, double.MaxValue, transactions),

                new TestCaseData(transactions, double.MaxValue, double.MinValue, new ITransaction[]{ }),

                new TestCaseData(transactions, 84.1, 150.24, new ITransaction[]{ }),

                new TestCaseData(transactions, 150.26, 340.01, new ITransaction[]{transactions[2], transactions[4]}),

                new TestCaseData(transactions, 250, 250, new ITransaction[]{transactions[2]}),

                new TestCaseData(transactions, 150.25, 340,
                    new ITransaction[]{transactions[1], transactions[2],transactions[4], transactions[5]}),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }
    }
}
