namespace _07.SpeedRacing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
   
    public class Car
    {
        private const double DefaultValueDouble = 0;
        public Car(string model,double fuelAmount,double fuelConsumptionPer1Km)
        {
            this.Model = model;
            this.FuelAmount = fuelAmount;
            this.FuelConsumptionPer1Km = fuelConsumptionPer1Km;
            this.DistanceTraveled = DefaultValueDouble;
        }

        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionPer1Km { get; set; }
        public double DistanceTraveled { get; set; }

    }
}
