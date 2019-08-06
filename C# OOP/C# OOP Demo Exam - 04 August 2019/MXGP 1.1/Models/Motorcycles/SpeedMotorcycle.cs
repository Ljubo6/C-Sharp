using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public class SpeedMotorcycle : SpeedMotorcucle
    {
        private const double InitializeCubicCentimeters = 125;
        private const int MinHorsePower = 50;
        private const int MaxHorsePower = 69;

        private int horsePower;
        public SpeedMotorcycle(string model, int horsePower)
            : base(model, horsePower, InitializeCubicCentimeters)
        {
        }

        public override int HorsePower
        {
            get => this.horsePower;
            protected set
            {
                if (value < MinHorsePower || value > MaxHorsePower)
                {
                    throw new ArgumentException($"Invalid horse power: {value}.");
                }
                this.horsePower = value;
            }
        }
    }
}
