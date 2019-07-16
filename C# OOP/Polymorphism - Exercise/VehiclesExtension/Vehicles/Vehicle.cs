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
        private double tankCapacity;
        public Vehicle(double fuelQuantity,double fuelConsumption,double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;            
        }
        public bool IsVehicleEmpty { get; set; }
        public double FuelQuantity
        {
            get
            {
                return this.fuelQuantity;
            }
            set
            {
                if (value > this.TankCapacity)
                {
                    value = 0;
                }
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
        public double TankCapacity
        {
            get
            {
                return this.tankCapacity;
            }
            set
            {
                this.tankCapacity = value;
            }
        }
        public virtual void Drive(double distance)
        {
            double currentFuelConsumption = this.FuelConsumption;

            double neededFuel = distance * this.FuelConsumption;
            if (this.FuelQuantity < neededFuel)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");                
            }
            this.FuelQuantity -= neededFuel;
            Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
        }

        public virtual void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (this.FuelQuantity + fuel > this.TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {fuel} fuel in the tank");
            }

            this.FuelQuantity += fuel;
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:F2}";
        }
    }
}
