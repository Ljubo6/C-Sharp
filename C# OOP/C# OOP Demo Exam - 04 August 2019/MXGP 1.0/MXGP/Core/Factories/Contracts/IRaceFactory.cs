using MXGP.Models.Races.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Factories.Contracts
{
    public interface IRaceFactory
    {
        IRace CreateRace(string name, int laps);
    }
}
