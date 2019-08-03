using ExtendedDatabase;
using NUnit.Framework;
using System;

namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private Person personOne;
        private Person personTwo;
        private Person personThree;
        private ExtendedDatabase.ExtendedDatabase database;
        [SetUp]
        public void Setup()
        {
            this.personOne = new Person(100, "Pesho");
            this.personTwo = new Person(101, "Gosho");
            this.personThree = new Person(102, "Ljubo");
            this.database = new ExtendedDatabase.ExtendedDatabase(this.personOne, this.personTwo);
        }

        [Test]
        public void TestPersonConstructor()
        {
            int expectedId = 100;
            string expectedName = "Pesho";
            Assert.AreEqual(expectedId, this.personOne.Id);
            Assert.AreEqual(expectedName, this.personOne.UserName);
        }
        [Test]
        public void TestDatabaseConstructor()
        {
            int expectedCount = 2;
            Assert.AreEqual(expectedCount, this.database.Count);
        }
        [Test]
        public void TestAddRangeException()
        {
            Person[] people = new Person[17];
            for (int i = 0; i < 17; i++)
            {
                people[i] = new Person(i, $"Name{i}");
            }
            Assert.Throws<ArgumentException>(() =>
            {
                this.database = new ExtendedDatabase.ExtendedDatabase(people);
            });
        }
        [Test]
        public void TestAddRangeExceptionWithEmptyPeople()
        {
            Person[] people = new Person[17];
            Assert.Throws<ArgumentException>(() =>
            {
                this.database = new ExtendedDatabase.ExtendedDatabase(people);
            });
        }
        [Test]
        public void TestAddNullPeople()
        {
            Person[] people = new Person[5];
            Assert.Throws<NullReferenceException>(() =>
            {
                this.database = new ExtendedDatabase.ExtendedDatabase(people);
            });
        }
        [Test]
        public void TestAddPersonCorrectly()
        {
            int expectedCount = 3;
            this.database.Add(personThree);
            Assert.AreEqual(expectedCount, this.database.Count);
        }
        [Test]
        public void TestAddPersonWithSameUserName()
        {
            Person person = new Person(103, "Pesho");
            Assert.Throws<InvalidOperationException>(() =>
          {
              this.database.Add(person);
          });
        }
        [Test]
        public void TestAddPersonWithSameId()
        {
            Person person = new Person(100, "Ivan");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Add(person);
            });
        }
        [Test]
        public void TestAddPersonToFullDatabase()
        {
            Person[] people = new Person[16];
            for (int i = 0; i < 16; i++)
            {
                people[i] = new Person(i, $"User{i}");
            }
            this.database = new ExtendedDatabase.ExtendedDatabase(people);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Add(personThree);
            });
        }
        [Test]
        public void TestIsRemoveDatabaseCorrectly()
        {
            int expectedCount = this.database.Count - 1;
            this.database.Remove();
            Assert.AreEqual(expectedCount, this.database.Count);
        }
        [Test]
        public void TestRemoveFromEmptyDatabase()
        {
            this.database = new ExtendedDatabase.ExtendedDatabase();
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Remove();
            });
        }
        [Test]
        public void TestIfNoUserIsPresentByThisUsername()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.FindByUsername(personThree.UserName);
            });
        }
        [Test]
        public void TestIfUsernameParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.database.FindByUsername(null);
            });
        }
        [Test]
        public void TestFindByUserNameCorrectly()
        {
            Person person = this.database.FindByUsername(personOne.UserName);
            Assert.AreEqual(person, this.personOne);
        }
        [Test]
        public void TestIfNoUserIsPresentByThisId()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.FindById(personThree.Id);
            });
        }
        [Test]
        public void TestIfNegativeIdsAreFound()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                this.database.FindById(-10);
            });
        }
        [Test]
        public void TestFindByIdCorrectly()
        {
            Person person = this.database.FindById(personOne.Id);
            Assert.AreEqual(person,this.personOne);
        }
    }
}