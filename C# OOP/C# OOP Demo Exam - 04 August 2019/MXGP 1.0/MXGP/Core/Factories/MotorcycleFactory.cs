using MXGP.Core.Factories.Contracts;
using MXGP.Models.Motorcycles;
using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core
{
    public class MotorcycleFactory : IMotorcycleFactory
    {
        public IMotorcycle CreateMotorcycle(string type, string model, int horsePower)
        {
            IMotorcycle motorcycle = null;
            switch (type)
            {
                case "Power":
                    motorcycle = new PowerMotorcycle(model, horsePower);
                    break;
                case "Speed":
                    motorcycle = new SpeedMotorcycle(model, horsePower);
                    break;
                default:
                    break;
            }
            return motorcycle;
        }

    }
}
