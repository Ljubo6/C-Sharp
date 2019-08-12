using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Players
{
    class MainPlayer : Player
    {
        private const int InitialLifePoints = 100;
        private const string InitialMainPlayerName = "Tommy Vercetti";
        public MainPlayer() 
            : base(InitialMainPlayerName, InitialLifePoints)
        {
        }
    }
}
