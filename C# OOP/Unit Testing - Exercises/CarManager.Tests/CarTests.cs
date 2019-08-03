using CarManager;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CarTests
    {
        private Car car;
        [SetUp]
        public void Setup()
        {
            this.car = new Car("Make","Model",10,10);
        }

        [Test]
        public void TestConstructorInstanceCorrectly()
        {
            Assert.AreEqual("Make",this.car.Make);
            Assert.AreEqual("Model",this.car.Model);
            Assert.AreEqual(10,this.car.FuelConsumption);
            Assert.AreEqual(10,this.car.FuelCapacity);
            Assert.AreEqual(0,this.car.FuelAmount);
        }
        [Test]
        public void TestMakeException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car = new Car(null, "Model", 10, 10);
            });
        }
        [Test]
        public void TestModeleException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car = new Car("Make", null, 10, 10);
            });
        }
        [Test]
        public void TestFuelConsumptionAxception()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car = new Car("Make", "Model", 0, 10);
            });
        }
        [Test]
        public void TestFuelCapacityException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car = new Car("Make", "Model", 10, 0);
            });
        }
        [Test]
        public void TestRefuelException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.car.Refuel(0);
            });
        }
        [Test]
        public void TestRefuelCorrectly()
        {
            this.car.Refuel(1);
            double expectedFuel = 1;
            Assert.AreEqual(expectedFuel,this.car.FuelAmount);
        }
        [Test]
        public void TestRefuelStatement()
        {
            this.car.Refuel(12);
            double expectedFuel = 10;
            Assert.AreEqual(expectedFuel, this.car.FuelAmount);
        }
        [Test]
        public void TestDriveException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.car.Drive(1000);
            });
        }
        [Test]
        public void TestDriveWorksCorrectly()
        {
            this.car.Refuel(1);
            this.car.Drive(1);
            double expectedFuelAmount = 0.9;
            Assert.AreEqual(expectedFuelAmount,this.car.FuelAmount);
        }
    }
}