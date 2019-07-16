using System;
using System.Collections.Generic;
using System.Text;
using VehiclesExtension.Vehicles.Contracts;

namespace VehiclesExtension.Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        private double fuelQuantity;
        private double fuelConsumption;
        public Vehicle(double fuelQuantity,double fuelConsumption)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }
        public double FuelQuantity
        {
            get
            {
                return this.fuelQuantity;
            }
            set
            {
                this.fuelQuantity = value;
            }
        }
        public double FuelConsumption
        {
            get
            {
                return this.fuelConsumption;
            }
            set
            {
                this.fuelConsumption = value;
            }
        }
        public void Drive(double distance)
        {
            double neededFuel = distance * this.FuelConsumption;
            if (this.FuelQuantity < neededFuel)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");                
            }
            this.FuelQuantity -= neededFuel;
            Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
        }

        public void Refuel(double fuel)
        {
            if (this is Truck)
            {
                fuel *= 0.95;
            }
            this.FuelQuantity += fuel;
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:F2}";
        }
    }
}
