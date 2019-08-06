using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Factories.Contracts
{
    public interface IMotorcycleFactory
    {
        IMotorcycle CreateMotorcycle(string type, string model, int horsePower);
    }
}
