namespace ExtendedDatabase.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [TestCaseSource("TestCaseConstructorWithValidData")]
        public void Constructor_ShouldPass_WithValidData(Person[] ctorPeople, int expectedCount)
        {
            Database db = new Database(ctorPeople);
            Assert.AreEqual(expectedCount, db.Count);
        }

        [TestCaseSource("TestCaseConstructorWithInvalidData")]
        public void Constructor_ThrowArgumentException_WithInvalidData(Person[] ctorPeople)
        {
            Assert.Throws<ArgumentException>(() => new Database(ctorPeople));
        }

        [TestCaseSource("TestCaseAddWithValidData")]
        public void Add_ShouldSucceed_WithValidData(Person[] ctorPeople, Person personToAdd, int expectedCount)
        {
            Database db = new Database(ctorPeople);
            db.Add(personToAdd);
            Assert.AreEqual(expectedCount, db.Count);
        }

        [TestCaseSource("TestCaseAddWithInvalidData")]
        public void Add_ShouldThrowException_WithInvalidData(Person[] ctorPeople, Person personToAdd)
        {
            Database db = new Database(ctorPeople);
            Assert.Throws<InvalidOperationException>(() => db.Add(personToAdd));
        }

        [TestCaseSource("TestCaseRemoveFromNonEmptyCollection")]
        public void Remove_ShouldReduceCount_WhenCollectionIsNotEmpty(Person[] ctorPeople, int expectedCount)
        {
            Database db = new Database(ctorPeople);
            db.Remove();
            Assert.AreEqual(expectedCount, db.Count);
        }

        [TestCaseSource("TestCaseRemoveFromEmptyCollection")]
        public void Remove_ShouldThrowException_WhenCollectionIsEmpty(Person[] ctorPeople, int peopleToRemove)
        {
            Database db = new Database(ctorPeople);
            for (int i = 0; i < peopleToRemove; i++)
            {
                db.Remove();
            }

            Assert.Throws<InvalidOperationException>(() => db.Remove());
        }

        [TestCaseSource("TestCaseFindByUsernameWithValidData")]
        public void FindByUsername_ShouldSucceed_WithValidData(Person[] ctorPeople, string personName)
        {
            Database db = new Database(ctorPeople);
            Person person = db.FindByUsername(personName);
            Assert.IsNotNull(person);
        }

        [TestCaseSource("TestCaseFindByUsernameWithMIssingName")]
        public void FindByUsername_ShouldThrow_InvalidOperationException_WithMissingName(Person[] ctorPeople, string personName)
        {
            Database db = new Database(ctorPeople);
            Assert.Throws<InvalidOperationException>(() => db.FindByUsername(personName));
        }

        [TestCaseSource("TestCaseFindByUsernameWithNullOrEmptyName")]
        public void FindByUsername_ShouldThrow_ArgumentNullException_WithInvalidData(Person[] ctorPeople, string personName)
        {
            Database db = new Database(ctorPeople);
            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(personName));
        }

        [TestCaseSource("TestCaseFindByIdWithValidData")]
        public void FindById_ShouldSucceed_WithValidData(Person[] ctorPeople, long personId)
        {
            Database db = new Database(ctorPeople);
            Person person = db.FindById(personId);
            Assert.IsNotNull(person);
        }

        [TestCaseSource("TestCaseFindByIdWithWithNegativeId")]
        public void FindById_ShouldThrow_ArgumentOutOfRangeException_WithNeagativeId(Person[] ctorPeople, long personId)
        {
            Database db = new Database(ctorPeople);
            Assert.Throws<ArgumentOutOfRangeException>(() => db.FindById(personId));
        }

        [TestCaseSource("TestCaseFindByIdWithWithMissingId")]
        public void FindById_ShouldThrow_InvalidOperationException_WithMissingId(Person[] ctorPeople, long personId)
        {
            Database db = new Database(ctorPeople);
            Assert.Throws<InvalidOperationException>(() => db.FindById(personId));
        }

        public static IEnumerable<TestCaseData> TestCaseConstructorWithValidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(new Person[]{ },0),

                new TestCaseData(new Person[]
                {
                    new Person(1,"Ivan"),
                    new Person(2,"George"),
                    new Person(3,"John")
                },3),

                new TestCaseData(new Person[]
                {
                    new Person(1,"Ivan"),
                    new Person(2,"George"),
                    new Person(3,"John"),
                    new Person(4,"Alan"),
                    new Person(5,"Correy"),
                    new Person(6,"Tim"),
                    new Person(7,"Michael"),
                    new Person(8,"Frank"),
                    new Person(9,"Allice"),
                    new Person(10,"Jacob"),
                    new Person(11,"Martin"),
                    new Person(12,"Greg"),
                    new Person(13,"Owen"),
                    new Person(14,"Paul"),
                    new Person(15,"Daniel"),
                    new Person(16,"Justin"),
                },16)
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseConstructorWithInvalidData()
        {
            yield return new TestCaseData(new object[]{new Person[]
                {
                    new Person(1,"Ivan"),
                    new Person(2,"George"),
                    new Person(3,"John"),
                    new Person(4,"Alan"),
                    new Person(5,"Correy"),
                    new Person(6,"Tim"),
                    new Person(7,"Michael"),
                    new Person(8,"Frank"),
                    new Person(9,"Allice"),
                    new Person(10,"Jacob"),
                    new Person(11,"Martin"),
                    new Person(12,"Greg"),
                    new Person(13,"Owen"),
                    new Person(14,"Paul"),
                    new Person(15,"Daniel"),
                    new Person(16,"Justin"),
                    new Person(17,"Aaa"),
                } });
        }

        public static IEnumerable<TestCaseData> TestCaseAddWithValidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(new Person[]{ }, new Person(1,"Timmy"),1),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },
                        new Person(4,"Gregg"),
                    4),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John"),
                        new Person(4,"Alan"),
                        new Person(5,"Correy"),
                        new Person(6,"Tim"),
                        new Person(7,"Michael"),
                        new Person(8,"Frank"),
                        new Person(9,"Allice"),
                        new Person(10,"Jacob"),
                        new Person(11,"Martin"),
                        new Person(12,"Greg"),
                        new Person(13,"Owen"),
                        new Person(14,"Paul"),
                        new Person(15,"Daniel")
                    },
                        new Person(16,"Justin"),
                    16)
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseAddWithInvalidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },
                        new Person(1,"Michael")),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },
                        new Person(4,"John")),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John"),
                        new Person(4,"Alan"),
                        new Person(5,"Correy"),
                        new Person(6,"Tim"),
                        new Person(7,"Michael"),
                        new Person(8,"Frank"),
                        new Person(9,"Allice"),
                        new Person(10,"Jacob"),
                        new Person(11,"Martin"),
                        new Person(12,"Greg"),
                        new Person(13,"Owen"),
                        new Person(14,"Paul"),
                        new Person(15,"Daniel"),
                        new Person(16,"Justin"),
                    },
                        new Person(17,"Paco"))
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseRemoveFromNonEmptyCollection()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },2),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                    },0),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John"),
                        new Person(4,"Alan"),
                        new Person(5,"Correy"),
                        new Person(6,"Tim"),
                        new Person(7,"Michael"),
                        new Person(8,"Frank"),
                        new Person(9,"Allice"),
                        new Person(10,"Jacob"),
                        new Person(11,"Martin"),
                        new Person(12,"Greg"),
                        new Person(13,"Owen"),
                        new Person(14,"Paul"),
                        new Person(15,"Daniel"),
                        new Person(16,"Justin"),
                    },15),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseRemoveFromEmptyCollection()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(new Person[]{ },0),

                new TestCaseData(new Person[]{new Person(1,"John") },1),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFindByUsernameWithValidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },"George"),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                    },"Ivan"),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFindByUsernameWithMIssingName()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },"Stephen"),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                    },"ivan"),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFindByUsernameWithNullOrEmptyName()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                    },null),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                    },string.Empty),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFindByIdWithValidData()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },2),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(324234324,"Ivan"),
                    },324234324),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(0,"Dimmy"),
                    },0),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFindByIdWithWithNegativeId()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John")
                    },-1),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(156131,"Ivan"),
                    },-156131),
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }

        public static IEnumerable<TestCaseData> TestCaseFindByIdWithWithMissingId()
        {
            TestCaseData[] testCases = new TestCaseData[]
            {
                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                    },213),

                new TestCaseData(
                    new Person[]
                    {
                        new Person(1,"Ivan"),
                        new Person(2,"George"),
                        new Person(3,"John"),
                        new Person(4,"Alan"),
                        new Person(5,"Correy"),
                        new Person(6,"Tim"),
                        new Person(7,"Michael"),
                        new Person(8,"Frank"),
                        new Person(9,"Allice"),
                        new Person(10,"Jacob"),
                        new Person(11,"Martin"),
                        new Person(12,"Greg"),
                        new Person(13,"Owen"),
                        new Person(14,"Paul"),
                        new Person(15,"Daniel")
                    },16)
            };

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }
    }
}