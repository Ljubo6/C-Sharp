using FightingArena;
using NUnit.Framework;
using System;

namespace Tests
{
    public class WarriorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            string expectedName = "Pesho";
            int expectedDmg = 15;
            int expectedHp = 100;
            Warrior warrior = new Warrior("Pesho",15,100);

            Assert.AreEqual(expectedName,warrior.Name);
            Assert.AreEqual(expectedDmg,warrior.Damage);
            Assert.AreEqual(expectedHp,warrior.HP);

        }

        [Test]
        public void TestWhitLikeEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("",25,100);
            });
        }
        [Test]
        public void TestWhitLikeWhiteSpaceName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("    ", 25, 100);
            });
        }
        [Test]
        public void TestWhitLikeZeroDamage()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Pesho", 0, 10);
            });
        }
        [Test]
        public void TestWhitLikeNegativeDamage()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Pesho", -10, 50);
            });
        }
        [Test]
        public void TestWhitLikeNegativeHp()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Pesho", 15, -10);
            });
        }
        [Test]
        public void TestIfAttackWorkCorrectly()
        {
            int expectedDefHp = 80;
            int expectedAttHp = 95;
            Warrior attacker = new Warrior("Pesho", 10, 100);
            Warrior deffender = new Warrior("Gosho", 5, 90);

            attacker.Attack(deffender);

            Assert.AreEqual(expectedAttHp,attacker.HP);
            Assert.AreEqual(expectedDefHp,deffender.HP);
        }
        [Test]
        public void TestAttackingWithLowHp()
        {
            Warrior attacker = new Warrior("Pesho", 10, 25);
            Warrior defender = new Warrior("Gosho", 5, 45);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });

        }
        [Test]
        public void TestAttackingEnemyWithLowHp()
        {
            Warrior attacker = new Warrior("Pesho", 10, 45);
            Warrior defender = new Warrior("Gosho", 5, 25);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });

        }
        [Test]
        public void TestAttackingStrongerEnemy()
        {
            Warrior attacker = new Warrior("Pesho", 10, 35);
            Warrior defender = new Warrior("Gosho", 40, 100);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });

        }
        [Test]
        public void TestKillingEnemy()
        {
            int expectedAttHp = 55;
            int expectedDefHp = 0;
            
            Warrior attacker = new Warrior("Pesho", 50, 100);
            Warrior defender = new Warrior("Gosho", 45, 40);

            attacker.Attack(defender);

            Assert.AreEqual(expectedAttHp, attacker.HP);
            Assert.AreEqual(expectedDefHp, defender.HP);

        }
    }
}