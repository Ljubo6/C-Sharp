namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Tests
    {
        private Phone phone;
        [SetUp]
        public void SetUp()
        {
            this.phone = new Phone("Make1","Model1");
        }
        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual("Make1",this.phone.Make);
            Assert.AreEqual("Model1",this.phone.Model);
            Assert.AreEqual(0,this.phone.Count);
        }
        [Test]
        public void TestMakeIsNullOrEmptyException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.phone = new Phone(null,"Model1");
            });
        }
        [Test]
        public void TestModelIsNullOrEmptyException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.phone = new Phone("Make1" ,null);
            });
        }
        [Test]
        public void TestAddContactsCorrectly()
        {
            this.phone.AddContact("Name1","Phone1");
            this.phone.AddContact("Name2","Phone2");
            Assert.AreEqual(2,this.phone.Count);
        }
        [Test]
        public void TestAddToExistingContacts()
        {
            this.phone.AddContact("Name1", "Phone1");
            this.phone.AddContact("Name2", "Phone2");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.phone.AddContact("Name1", "Phone1");
            });
        }
        [Test]
        public void TestCallingCorrectly()
        {
            this.phone.AddContact("Name1", "Phone1");
            this.phone.AddContact("Name2", "Phone2");
            Assert.AreEqual($"Calling Name1 - Phone1...",this.phone.Call("Name1"));
        }
        [Test]
        public void TestAddContactException()
        {
            this.phone.AddContact("Name1", "Phone1");
            this.phone.AddContact("Name2", "Phone2");

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.phone.Call("Name3");
            });
        }
    }
}