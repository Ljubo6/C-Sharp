using SpaceStation.Models.Astronauts.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models
{
    public class Biologist : Astronaut
    {
        private const double InitialOxygene = 70;
        public Biologist(string name) 
            : base(name, InitialOxygene)
        {
        }
        public override void Breath()
        {
            this.Oxygen -= 5;
        }
    }
}
