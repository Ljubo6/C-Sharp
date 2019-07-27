using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Cards.Models
{
    public class TrapCard : Card
    {
        private const int InitialDamagePionts = 120;
        private const int InitialHealthPionts = 5;

        public TrapCard(string name) 
            : base(name, InitialDamagePionts, InitialHealthPionts)
        {
        }
    }
}
