using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Cards.Models
{
    public class MagicCard : Card
    {
        private const int InitialDamagePionts = 5;
        private const int InitielHealthPionts = 80;
        public MagicCard(string name) 
            : base(name, InitialDamagePionts, InitielHealthPionts)
        {
        }
    }
}
