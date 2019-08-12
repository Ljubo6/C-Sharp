namespace BlueOrigin.Tests
{
    using System;
    using NUnit.Framework;

    public class SpaceshipTests
    {
        private Astronaut astronaut;
        private Astronaut astronaut2;
        private Astronaut astronaut3;
        private Spaceship spaceship;
        [SetUp]
        public void SetUp()
        {
            this.astronaut = new Astronaut("Pesho", 2.5);
            this.astronaut2 = new Astronaut("Gosho", 5.0);
            this.astronaut3 = new Astronaut("Ivan", 4.0);

            this.spaceship = new Spaceship("Galactica", 2);
        }
        [Test]
        public void TestCreateAstronautCorrectly()
        {
            Assert.AreEqual("Pesho", astronaut.Name);
            Assert.AreEqual(2.5, astronaut.OxygenInPercentage);
        }
        [Test]
        public void TestThrowNullException()
        {
            Assert.Throws<ArgumentNullException>(() => spaceship = new Spaceship(null, 100));
        }
        [Test]
        public void TestThrowEmptyException()
        {
            Assert.Throws<ArgumentNullException>(() => spaceship = new Spaceship("", 100));
        }
        [Test]
        public void TestCreateSpaceshipCorrectly()
        {
            Assert.AreEqual(0,spaceship.Count);
        }
        [Test]
        public void TestThrowEnvalidcapacityException()
        {
            Assert.Throws<ArgumentException>(() => spaceship = new Spaceship("Space",-1));
        }
        [Test]
        public void TestAddAstronautMethodWorksCorrectly()
        {
            this.spaceship.Add(astronaut);
            this.spaceship.Add(astronaut2);
            Assert.AreEqual(2,spaceship.Count);
        }
        [Test]
        public void TestAddAstronautMethodThrowCountException()
        {
            this.spaceship.Add(astronaut);
            this.spaceship.Add(astronaut2);
            Assert.Throws<InvalidOperationException>(() => this.spaceship.Add(astronaut3));
        }
        [Test]
        public void TestAddAstronautMethotThrowWxceptionAstroExist()
        {
            this.spaceship.Add(astronaut);
            Assert.Throws<InvalidOperationException>(() => this.spaceship.Add(astronaut));
        }
        [Test]
        public void TestSpaceshipRemoveMethodWorkscorrectly()
        {
            this.spaceship.Add(astronaut);
            Assert.IsTrue(spaceship.Remove(astronaut.Name));
        }
    }
}