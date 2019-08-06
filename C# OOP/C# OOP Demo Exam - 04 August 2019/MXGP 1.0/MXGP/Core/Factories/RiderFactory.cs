using MXGP.Core.Factories.Contracts;
using MXGP.Models.Races.Contracts;
using MXGP.Models.Riders;
using MXGP.Models.Riders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core
{
    public class RiderFactory : IRiderFactory
    {
        public IRider CreateRider(string name)
        {
            IRider rider = new Rider(name);
            return rider;
        }
    }
}
