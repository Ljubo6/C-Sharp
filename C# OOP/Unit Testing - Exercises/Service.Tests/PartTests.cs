using NUnit.Framework;
using Service.Models.Parts;
using System;

namespace Tests
{
    public class PartTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void LaptopPartShouldBeCreatedSuccessfully()
        {
            var part = new LaptopPart("LaptopPart", 5.0m);
            var expectedName = "LaptopPart";
            var expectedCost = 7.5m;
            var expectedBrokenBool = false;

            Assert.AreEqual(expectedName, part.Name);
            Assert.AreEqual(expectedCost, part.Cost);
            Assert.AreEqual(expectedBrokenBool, part.IsBroken);
        }
        [Test]
        public void PCPartShouldBeCreatedSuccessfully()
        {
            var part = new PCPart("PCPart", 5.0m);
            var expectedName = "PCPart";
            var expectedCost = 6.0m;
            var expectedBrokenBool = false;

            Assert.AreEqual(expectedName, part.Name);
            Assert.AreEqual(expectedCost, part.Cost);
            Assert.AreEqual(expectedBrokenBool, part.IsBroken);
        }
        [Test]
        public void PhonePartShouldBeCreatedSuccessfully()
        {
            var part = new PhonePart("PhonePart", 5.0m);
            var expectedName = "PhonePart";
            var expectedCost = 6.5m;
            var expectedBrokenBool = false;

            Assert.AreEqual(expectedName, part.Name);
            Assert.AreEqual(expectedCost, part.Cost);
            Assert.AreEqual(expectedBrokenBool, part.IsBroken);
        }


        [Test]
        public void LaptopPartShouldBeThrowExceptionIfNameIsNull()
        {
            Assert.Throws<ArgumentException>(() => new LaptopPart(null, 5.0m));
        }
        [Test]
        public void LaptopPartShouldBeThrowExceptionIfNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new LaptopPart("", 5.0m));
        }
        [Test]
        public void LaptopPartShouldBeThrowExceptionIfCostIsZero()
        {
            Assert.Throws<ArgumentException>(() => new LaptopPart("LaptopPart", 0));
        }
        [Test]
        public void LaptopPartShouldBeThrowExceptionIfCostIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new LaptopPart("LaptopPart", -5.0m));
        }



        [Test]
        public void PCPartShouldBeThrowExceptionIfNameIsNull()
        {
            Assert.Throws<ArgumentException>(() => new PCPart(null, 5.0m));
        }
        [Test]
        public void PCPartShouldBeThrowExceptionIfNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new PCPart("", 5.0m));
        }
        [Test]
        public void PCPartShouldBeThrowExceptionIfCostIsZero()
        {
            Assert.Throws<ArgumentException>(() => new PCPart("PCPart", 0));
        }
        [Test]
        public void PCPartShouldBeThrowExceptionIfCostIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new PCPart("PCPart", -5.0m));
        }



        [Test]
        public void PhonePartShouldBeThrowExceptionIfNameIsNull()
        {
            Assert.Throws<ArgumentException>(() => new PhonePart(null, 5.0m));
        }
        [Test]
        public void PhonePartShouldBeThrowExceptionIfNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new PhonePart("", 5.0m));
        }
        [Test]
        public void PhonePartShouldBeThrowExceptionIfCostIsZero()
        {
            Assert.Throws<ArgumentException>(() => new PhonePart("PhonePart", 0));
        }
        [Test]
        public void PhonePartShouldBeThrowExceptionIfCostIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new PhonePart("PhonePart", -5.0m));
        }


        [Test]
        public void LaptopPartShouldRepairWorksCorrectly()
        {
            var part = new LaptopPart("LaptopPart", 5.0m, true);

            part.Repair();

            Assert.IsFalse(part.IsBroken);
        }
        [Test]
        public void PCPartShouldRepairWorksCorrectly()
        {
            var part = new LaptopPart("PCPart", 5.0m, true);

            part.Repair();

            Assert.IsFalse(part.IsBroken);
        }
        [Test]
        public void PhonePartShouldRepairWorksCorrectly()
        {
            var part = new LaptopPart("PhonePart", 5.0m, true);

            part.Repair();

            Assert.IsFalse(part.IsBroken);
        }


        [Test]
        public void LaptopPartShouldGivenCorrectInfo()
        {
            var part = new LaptopPart("LaptopPart",5.0m,false);

            var expectedReport = $"LaptopPart - 7.50$" + Environment.NewLine +
                $"Broken: False";
            Assert.AreEqual(expectedReport,part.Report());
        }
        [Test]
        public void PCPartShouldGivenCorrectInfo()
        {
            var part = new PCPart("PCPart", 5.0m, true);

            var expectedReport = $"PCPart - 6.00$" + Environment.NewLine +
                $"Broken: True";
            Assert.AreEqual(expectedReport, part.Report());
        }
        [Test]
        public void PhonePartShouldGivenCorrectInfo()
        {
            var part = new PhonePart("PhonePart", 5.0m, false);

            var expectedReport = $"PhonePart - 6.50$" + Environment.NewLine +
                $"Broken: False";
            Assert.AreEqual(expectedReport, part.Report());
        }
    }
}