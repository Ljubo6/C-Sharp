using NUnit.Framework;
using System;

namespace TheRace.Tests
{
    [TestFixture]
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;
        [SetUp]
        public void Setup()
        {
            this.raceEntry = new RaceEntry();
        }
        [Test]
        public void ConstructorShouldInitializeDictionary()
        {
            Assert.IsNotNull(this.raceEntry);
        }
        [Test]
        public  void TestAddRiderCorrectly()
        {
            var rider = new UnitRider("Pesho", new UnitMotorcycle("Honda", 35, 50));
            this.raceEntry.AddRider(rider);

            Assert.AreEqual(1,this.raceEntry.Counter);
        }
        [Test]
        public void TestAddRiderWithNullEntry()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.raceEntry.AddRider(null);
            });
        }
        [Test]
        public void TestIfRiderToAddAlreadyExistException()
        {
            var rider = new UnitRider("Pesho",new UnitMotorcycle("Honda", 35, 50));
            this.raceEntry.AddRider(rider);
            
            Assert.Throws<InvalidOperationException> (()=> this.raceEntry.AddRider(rider));
        }
        [Test]
        public void TestForCorrectMessageWhenAddRiderCorrectly()
        {
            var rider = new UnitRider("Pesho",new UnitMotorcycle("Honda", 35, 50));

            string expectedMessage = "Rider Pesho added in race.";
            string actualMessage = this.raceEntry.AddRider(rider);

            Assert.AreEqual(expectedMessage,actualMessage);
        }
        [Test]
        public void TestCalculateAverageHorsePowerWorkCorrectly()
        {
            var rider1 = new UnitRider("Pesho", new UnitMotorcycle("Honda", 40, 50));
            var rider2 = new UnitRider("Gosho", new UnitMotorcycle("Honda", 35, 50));
            this.raceEntry.AddRider(rider1);
            this.raceEntry.AddRider(rider2);
            double expectedResult = 37.5;
            double actualResult = this.raceEntry.CalculateAverageHorsePower();

            Assert.AreEqual(expectedResult,actualResult);
        }
        [Test]
        public void TestWillThrowExceptionWhenRidersAreLessTwo()
        {
            var rider = new UnitRider("Pesho",new UnitMotorcycle("Honda", 35, 50));
            this.raceEntry.AddRider(rider);

            Assert.Throws<InvalidOperationException>(() => this.raceEntry.CalculateAverageHorsePower());
        }


    }
}