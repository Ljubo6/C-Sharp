using System;
using System.Collections.Generic;
using System.Text;
using PlayersAndMonsters.Repositories.Contracts;

namespace PlayersAndMonsters.Models.Players.Models
{
    public class Beginner : Player
    {
        private const int InitialHealth = 50;
        public Beginner(ICardRepository cardRepository, string username) 
            : base(cardRepository, username, InitialHealth)
        {
        }
    }
}
