using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public class PowerMotorcycle : Motorcycle
    {
        private const double InitializeCubicCentimeters = 450;
        private const int MinHorsePower = 70;
        private const int MaxHorsePower = 100;

        private int horsePower;
        public PowerMotorcycle(string model, int horsePower) 
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
                    throw new ArgumentException($"Invalid horse power: {this.horsePower}.");
                }
                this.horsePower = value;
            }
            
        }

    }
}
