using NUnit.Framework;
using Service.Models.Devices;
using Service.Models.Parts;
using System;

namespace Service.Tests
{
    public class DeviceTests
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
        public void LaptopShouldBeCreatedSuccessfully()
        {
            var device = new Laptop("Dell");
            var expectedMake = "Dell";
            Assert.AreEqual(expectedMake, device.Make);
        }
        [Test]
        public void PCShouldBeCreatedSuccessfully()
        {
            var device = new PC("Acer");
            var expectedMake = "Acer";
            Assert.AreEqual(expectedMake, device.Make);
        }
        [Test]
        public void PhoneShouldBeCreatedSuccessfully()
        {
            var device = new Phone("iPhone");
            var expectedMake = "iPhone";
            Assert.AreEqual(expectedMake, device.Make);
        }



        [Test]
        public void LaptopShouldThrowExceptionIfNull()
        {
            Assert.Throws<ArgumentException>(() => new Laptop(null));
        }
        [Test]
        public void LaptopShouldThrowExceptionIfEmpty()
        {
            Assert.Throws<ArgumentException>(() => new Laptop(""));
        }
        [Test]
        public void PCShouldThrowExceptionIfNull()
        {
            Assert.Throws<ArgumentException>(() => new PC(null));
        }
        [Test]
        public void PCShouldThrowExceptionIfEmpty()
        {
            Assert.Throws<ArgumentException>(() => new PC(""));
        }
        [Test]
        public void PhoneShouldThrowExceptionIfNull()
        {
            Assert.Throws<ArgumentException>(() => new Phone(null));
        }
        [Test]
        public void PhoneShouldThrowExceptionIfEmpty()
        {
            Assert.Throws<ArgumentException>(() => new Phone(""));
        }


        [Test]
        public void LaptopAddPartShouldThrowExceptionIfParTypeIsWrong()
        {
            var device = new Laptop("Laptop");
            var pcPart = new PCPart("PCpart",5.0m);
            Assert.Throws<InvalidOperationException>(() => device.AddPart(pcPart));
        }
        [Test]
        public void LaptopAddPartShouldThrowExceptionIfPartAlreadyExist()
        {
            var device = new Laptop("Laptop");
            var laptopPart = new LaptopPart("LaptopPart", 5.0m);
            device.AddPart(laptopPart);
            Assert.Throws<InvalidOperationException>(() => device.AddPart(laptopPart));
        }
        [Test]
        public void LaptopAddPartShouldBeWorksCorrectly()
        {
            var device = new Laptop("Laptop");
            var laptopPart1 = new LaptopPart("LaptopPart1", 5.0m);
            var laptopPart2 = new LaptopPart("LaptopPart2", 3.0m);
            device.AddPart(laptopPart1);
            device.AddPart(laptopPart2);

            var expectedPartCount = 2;

            Assert.AreEqual(expectedPartCount,device.Parts.Count);

        }




        [Test]
        public void PCAddPartShouldThrowExceptionIfParTypeIsWrong()
        {
            var device = new PC("PC");
            var laptopPart = new LaptopPart("LaptopPart", 5.0m);
            Assert.Throws<InvalidOperationException>(() => device.AddPart(laptopPart));
        }
        [Test]
        public void PCAddPartShouldThrowExceptionIfPartAlreadyExist()
        {
            var device = new PC("PC");
            var pcPart = new PCPart("PCPart", 5.0m);
            device.AddPart(pcPart);
            Assert.Throws<InvalidOperationException>(() => device.AddPart(pcPart));
        }
        [Test]
        public void PCAddPartShouldBeWorksCorrectly()
        {
            var device = new PC("PC");
            var pcPart1 = new PCPart("PCPart1", 5.0m);
            var pcPart2 = new PCPart("PCPart2", 3.0m);
            device.AddPart(pcPart1);
            device.AddPart(pcPart2);

            var expectedPartCount = 2;

            Assert.AreEqual(expectedPartCount, device.Parts.Count);

        }



        [Test]
        public void PhoneAddPartShouldThrowExceptionIfParTypeIsWrong()
        {
            var device = new Phone("Phone");
            var pcPart = new PCPart("PCpart", 5.0m);
            Assert.Throws<InvalidOperationException>(() => device.AddPart(pcPart));
        }
        [Test]
        public void phoneAddPartShouldThrowExceptionIfPartAlreadyExist()
        {
            var device = new Phone("Phone");
            var phonePart = new PhonePart("PhonePart", 5.0m);
            device.AddPart(phonePart);
            Assert.Throws<InvalidOperationException>(() => device.AddPart(phonePart));
        }
        [Test]
        public void PhoneAddPartShouldBeWorksCorrectly()
        {
            var device = new Phone("Phone");
            var phonePart1 = new PhonePart("PhonePart1", 5.0m);
            var phonePart2 = new PhonePart("PhonePart2", 3.0m);
            device.AddPart(phonePart1);
            device.AddPart(phonePart2);

            var expectedPartCount = 2;

            Assert.AreEqual(expectedPartCount, device.Parts.Count);

        }



        [Test]
        public void LaptopRemovePartShouldThrowExceptionIfPartNameIsNull()
        {
            var device = new Laptop("Laptop");
            Assert.Throws<ArgumentException>(() => device.RemovePart(null));
        }
        [Test]
        public void LaptopRemovePartShouldThrowExceptionIfPartNameIsEmpty()
        {
            var device = new Laptop("Laptop");
            Assert.Throws<ArgumentException>(() => device.RemovePart(""));
        }
        [Test]
        public void LaptopRemovePartShouldThrowExceptionIfRemoveNonExistingPartName()
        {
            var device = new Laptop("Laptop");
            var laptopPart = new LaptopPart("LaptopPart",3.5m);
            device.AddPart(laptopPart);
            Assert.Throws<InvalidOperationException>(() => device.RemovePart("pcPart"));
        }
        [Test]
        public void LaptopRemovePartShouldWorksCorrectly()
        {
            var device = new Laptop("Laptop");
            var laptopPart1 = new LaptopPart("LaptopPart1", 3.0m);
            var laptopPart2 = new LaptopPart("LaptopPart2", 3.5m);
            device.AddPart(laptopPart1);
            device.AddPart(laptopPart2);

            device.RemovePart(laptopPart1.Name);

            var excpectedDevicePartCount = 1;
            Assert.AreEqual(excpectedDevicePartCount,device.Parts.Count);

        }


        [Test]
        public void PCRemovePartShouldThrowExceptionIfPartNameIsNull()
        {
            var device = new PC("PC");
            Assert.Throws<ArgumentException>(() => device.RemovePart(null));
        }
        [Test]
        public void PCRemovePartShouldThrowExceptionIfPartNameIsEmpty()
        {
            var device = new PC("PC");
            Assert.Throws<ArgumentException>(() => device.RemovePart(""));
        }
        [Test]
        public void PCRemovePartShouldThrowExceptionIfRemoveNonExistingPartName()
        {
            var device = new PC("PC");
            var pcPart = new PCPart("PCPart", 3.5m);
            device.AddPart(pcPart);
            Assert.Throws<InvalidOperationException>(() => device.RemovePart("laptopPart"));
        }
        [Test]
        public void PCRemovePartShouldWorksCorrectly()
        {
            var device = new PC("PC");
            var pcPart1 = new PCPart("PCPart1", 3.0m);
            var pcPart2 = new PCPart("PCPart2", 3.5m);
            device.AddPart(pcPart1);
            device.AddPart(pcPart2);

            device.RemovePart(pcPart1.Name);

            var excpectedDevicePartCount = 1;
            Assert.AreEqual(excpectedDevicePartCount, device.Parts.Count);

        }



        [Test]
        public void PhoneRemovePartShouldThrowExceptionIfPartNameIsNull()
        {
            var device = new Phone("Phone");
            Assert.Throws<ArgumentException>(() => device.RemovePart(null));
        }
        [Test]
        public void PhoneRemovePartShouldThrowExceptionIfPartNameIsEmpty()
        {
            var device = new Phone("Phone");
            Assert.Throws<ArgumentException>(() => device.RemovePart(""));
        }
        [Test]
        public void PhoneRemovePartShouldThrowExceptionIfRemoveNonExistingPartName()
        {
            var device = new Phone("Phone");
            var phonePart = new PhonePart("PhonePart", 3.5m);
            device.AddPart(phonePart);
            Assert.Throws<InvalidOperationException>(() => device.RemovePart("laptopPart"));
        }
        [Test]
        public void PhoneRemovePartShouldWorksCorrectly()
        {
            var device = new Phone("Phone");
            var phonePart1 = new PhonePart("PhonePart1", 3.0m);
            var phonePart2 = new PhonePart("PhonePart2", 3.5m);
            device.AddPart(phonePart1);
            device.AddPart(phonePart2);

            device.RemovePart(phonePart1.Name);

            var excpectedDevicePartCount = 1;
            Assert.AreEqual(excpectedDevicePartCount, device.Parts.Count);

        }



        [Test]
        public void LaptopRepairPartShouldThrowExceptionIfPartNameIsNull()
        {
            var device = new Laptop("Laptop");
            Assert.Throws<ArgumentException>(() => device.RepairPart(null));
        }
        [Test]
        public void LaptopRepairPartShouldThrowExceptionIfPartNameIsEmpty()
        {
            var device = new Laptop("Laptop");
            Assert.Throws<ArgumentException>(() => device.RepairPart(""));
        }
        [Test]
        public void LaptopRepairPartShouldThrowExceptionIfRemoveNonExistingPartName()
        {
            var device = new Laptop("Laptop");
            var laptopPart = new LaptopPart("LaptopPart",5.0m);
            device.AddPart(laptopPart);

            Assert.Throws<InvalidOperationException>(() => device.RepairPart("pcPart"));
        }
        [Test]
        public void LaptopRepairPartShouldThrowExceptionIfRemoveNonBrokingPartName()
        {
            var device = new Laptop("Laptop");
            var laptopPart = new LaptopPart("LaptopPart", 5.0m,false);
            device.AddPart(laptopPart);

            Assert.Throws<InvalidOperationException>(() => device.RepairPart("LaptopPart"));
        }
        [Test]
        public void LaptopRepairPartShouldWorksCorrectly()
        {
            var device = new Laptop("Laptop");
            var laptopPart = new LaptopPart("LaptopPart", 5.0m, true);
            device.AddPart(laptopPart);
            device.RepairPart(laptopPart.Name);

            Assert.IsFalse(laptopPart.IsBroken);
        }



        [Test]
        public void PCRepairPartShouldThrowExceptionIfPartNameIsNull()
        {
            var device = new PC("PC");
            Assert.Throws<ArgumentException>(() => device.RepairPart(null));
        }
        [Test]
        public void PCRepairPartShouldThrowExceptionIfPartNameIsEmpty()
        {
            var device = new PC("PC");
            Assert.Throws<ArgumentException>(() => device.RepairPart(""));
        }
        [Test]
        public void PCRepairPartShouldThrowExceptionIfRemoveNonExistingPartName()
        {
            var device = new PC("PC");
            var pcPart = new PCPart("PCPart", 5.0m);
            device.AddPart(pcPart);

            Assert.Throws<InvalidOperationException>(() => device.RepairPart("laptopPart"));
        }
        [Test]
        public void PCRepairPartShouldThrowExceptionIfRemoveNonBrokingPartName()
        {
            var device = new PC("PC");
            var pcPart = new PCPart("PCPart", 5.0m, false);
            device.AddPart(pcPart);

            Assert.Throws<InvalidOperationException>(() => device.RepairPart("pcPart"));
        }
        [Test]
        public void PCRepairPartShouldWorksCorrectly()
        {
            var device = new PC("PC");
            var pcPart = new PCPart("pcPart", 5.0m, true);
            device.AddPart(pcPart);
            device.RepairPart(pcPart.Name);

            Assert.IsFalse(pcPart.IsBroken);
        }



        [Test]
        public void PhoneRepairPartShouldThrowExceptionIfPartNameIsNull()
        {
            var device = new Phone("Phone");
            Assert.Throws<ArgumentException>(() => device.RepairPart(null));
        }
        [Test]
        public void PhoneRepairPartShouldThrowExceptionIfPartNameIsEmpty()
        {
            var device = new Phone("Phone");
            Assert.Throws<ArgumentException>(() => device.RepairPart(""));
        }
        [Test]
        public void PhoneRepairPartShouldThrowExceptionIfRemoveNonExistingPartName()
        {
            var device = new Phone("Phone");
            var phonePart = new PhonePart("PhonePart", 5.0m);
            device.AddPart(phonePart);

            Assert.Throws<InvalidOperationException>(() => device.RepairPart("laptopPart"));
        }
        [Test]
        public void PhoneRepairPartShouldThrowExceptionIfRemoveNonBrokingPartName()
        {
            var device = new Phone("Phone");
            var phonePart = new PhonePart("PhonePart", 5.0m, false);
            device.AddPart(phonePart);

            Assert.Throws<InvalidOperationException>(() => device.RepairPart("phonePart"));
        }
        [Test]
        public void PhoneRepairPartShouldWorksCorrectly()
        {
            var device = new Phone("Phone");
            var phonePart = new PhonePart("PhonePart", 5.0m, true);
            device.AddPart(phonePart);
            device.RepairPart(phonePart.Name);

            Assert.IsFalse(phonePart.IsBroken);
        }
    }
}
