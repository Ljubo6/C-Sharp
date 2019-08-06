using MXGP.Models.Riders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Factories.Contracts
{
    public interface IRiderFactory
    {
        IRider CreateRider(string riderName);
    }
}
