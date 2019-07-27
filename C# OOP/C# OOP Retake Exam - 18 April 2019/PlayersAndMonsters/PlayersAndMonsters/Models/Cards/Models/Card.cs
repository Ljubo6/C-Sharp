using PlayersAndMonsters.Common;
using PlayersAndMonsters.Models.Cards.Contracts;
using System;

namespace PlayersAndMonsters.Models.Cards.Models
{
    public abstract class Card : ICard
    {
        private string name;
        private int damagePionts;
        private int healthPoints;
        protected Card(string name,int damagePionts,int healthPoints)
        {
            this.Name = name;
            this.DamagePoints = damagePionts;
            this.HealthPoints = healthPoints;
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.NullCardName);
                }
                this.name = value;
            }
        }

        public int DamagePoints
        {
            get
            {
                return this.damagePionts;
            }
            set
            {
                if (damagePionts < 0)
                {
                    throw new ArgumentException(ExceptionMessages.NegativeCardDamagePoints);
                }
                this.damagePionts = value;
            }         
        }

        public int HealthPoints
        {
            get
            {
                return this.healthPoints;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.NegativeCardHealthPoints);
                }
                this.healthPoints = value;
            }
        }
    }
}
