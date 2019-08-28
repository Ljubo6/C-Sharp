using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models
{
    public  class Geodesist : Astronaut
    {
        private const double InitialOxygene = 50;
        public Geodesist(string name) 
            : base(name, InitialOxygene)
        {
        }

    }
}
