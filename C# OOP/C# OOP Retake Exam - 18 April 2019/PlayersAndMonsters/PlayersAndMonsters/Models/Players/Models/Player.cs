﻿namespace PlayersAndMonsters.Models.Players.Models
{
    using PlayersAndMonsters.Common;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Repositories.Contracts;
    using System;

    public abstract class Player : IPlayer
    {
        private string username;
        private int health;

        protected Player(ICardRepository cardRepository,string username,int health)
        {
            this.CardRepository = cardRepository;
            this.Username = username;
            this.Health = health;
        }
        public ICardRepository CardRepository { get; private set; }
        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.NullPlayerUsername);
                }
                this.username = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.NegativePlayeHealth);
                }
                this.health = value;
            }
        }

        public bool IsDead { get; private set; }

        public void TakeDamage(int damagePoints)
        {
            if (damagePoints < 0)
            {
                throw new ArgumentException(ExceptionMessages.NegativeDamagePoints);
            }
            if (this.Health - damagePoints < 0)
            {
                this.Health = 0;
                IsDead = true;
            }
            else
            {
                this.Health -= damagePoints;
            }
        }
    }
}
