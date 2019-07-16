using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclesExtension.Vehicles
{
    public class Truck : Vehicle
    {
        private const double airConditionConsumption = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption) 
            : base(fuelQuantity, fuelConsumption)
        {
            this.FuelConsumption += airConditionConsumption;
        }
    }
}
