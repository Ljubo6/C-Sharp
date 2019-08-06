using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public abstract class Motorcycle : IMotorcycle
    {
        private string model;
        protected Motorcycle(string model,int horsePower,double cubicCantimeters)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCantimeters;
        }
        public string Model
        {
            get
            {
                return this.model;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException($"Model {this.Model} cannot be less than 4 symbols.");
                }
                this.model = value;
            }

        }

        public abstract int HorsePower { get; protected set; }


        public double CubicCentimeters { get; private set; }


        public double CalculateRacePoints(int laps) => this.CubicCentimeters / this.HorsePower * laps;
    }
}
