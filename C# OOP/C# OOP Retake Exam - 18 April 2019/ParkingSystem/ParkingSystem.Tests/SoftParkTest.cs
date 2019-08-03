namespace ParkingSystem.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class SoftParkTest
    {
        private Car car;
        private SoftPark parking;
        [SetUp]
        public void Setup()
        {
            this.car = new Car("Make", "Number");
            this.parking = new SoftPark();
        }

        [Test]
        public void TestCarConstructor()
        {
            Assert.AreEqual("Make", this.car.Make);
            Assert.AreEqual("Number", this.car.RegistrationNumber);

        }
        [Test]
        public void TestSoftParkConstructor()
        {
            int expectedCount = 12;
            Assert.AreEqual(expectedCount, this.parking.Parking.Count);
        }
        [Test]
        public void TestAddCarToNotExistingSpot()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.parking.ParkCar("D3", this.car);
            });
        }
        [Test]
        public void TestAddCarToTakenSpot()
        {
            this.parking.ParkCar("A1", this.car);
            Car newCar = new Car("Make1", "Number1");
            Assert.Throws<ArgumentException>(() =>
            {
                this.parking.ParkCar("A1", newCar);
            });
        }
        [Test]
        public void TestParkCarWhoIsAllreadyParked()
        {
            this.parking.ParkCar("A1", this.car);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.parking.ParkCar("A2", this.car);
            });
        }
        [Test]
        public void TestParkCarCorrectly()
        {
            Assert.AreEqual($"Car:{car.RegistrationNumber} parked successfully!",
                this.parking.ParkCar("A1", this.car));
        }
        [Test]
        public void TestRemoveCarFromNonExistingSpot()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                this.parking.RemoveCar("D3", this.car);
            });
        }
        [Test]
        public void TestRemoveNonExistingCar()
        {
            this.parking.ParkCar("A1",this.car);
            Car newCar = new Car("Model1","Number1");
            Assert.Throws<ArgumentException>(() =>
            {
                this.parking.RemoveCar("A1", newCar);
            });
        }
        [Test]
        public void TestRemoveNormalParkingCar()
        {
            this.parking.ParkCar("A1", this.car);
            Assert.AreEqual($"Remove car:{car.RegistrationNumber} successfully!",
                this.parking.RemoveCar("A1", this.car));
        }
    }
}