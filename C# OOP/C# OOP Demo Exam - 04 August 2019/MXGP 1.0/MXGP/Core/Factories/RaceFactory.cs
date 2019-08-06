using MXGP.Core.Factories.Contracts;
using MXGP.Models.Races;
using MXGP.Models.Races.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core
{
    public class RaceFactory : IRaceFactory
    {
        public IRace CreateRace(string name,int laps)
        {
            IRace race = new Race(name,laps);
            return race;
        }
    }
}
