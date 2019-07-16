using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclesExtension.Vehicles
{
    public class Bus : Vehicle
    {
        private const double airConditionConsumption = 1.4;

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override void Drive(double distance)
        {

            double currentFuelConsumption = this.FuelConsumption;

            if (!IsVehicleEmpty)
            {
                currentFuelConsumption += airConditionConsumption;
            }
            double neededFuel = distance * currentFuelConsumption;
            if (this.FuelQuantity < neededFuel)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }
            this.FuelQuantity -= neededFuel;
            Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
        }
    }
}
