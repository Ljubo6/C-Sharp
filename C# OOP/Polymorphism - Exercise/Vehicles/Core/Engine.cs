using System;
using System.Collections.Generic;
using System.Text;
using VehiclesExtension.Vehicles;
using VehiclesExtension.Vehicles.Contracts;

namespace VehiclesExtension.Core
{
    public class Engine
    {
        public void Run()
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carFuelConsumptuion = double.Parse(carInfo[2]);

            double trucklQuantity = double.Parse(truckInfo[1]);
            double trucklConsumptuion = double.Parse(truckInfo[2]);

            IVehicle car = new Car(carFuelQuantity, carFuelConsumptuion);
            IVehicle truck = new Truck(trucklQuantity,trucklConsumptuion);

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] inputArgs = Console.ReadLine().Split();
                    string action = inputArgs[0];
                    string vehicleType = inputArgs[1];

                    double value = double.Parse(inputArgs[2]);

                    if (action == "Refuel")
                    {
                        if (vehicleType == "Car")
                        {
                            car.Refuel(value);
                        }
                        else
                        {
                            truck.Refuel(value);
                        }
                    }
                    else
                    {
                        if (vehicleType == "Car")
                        {
                            car.Drive(value);
                        }
                        else
                        {
                            truck.Drive(value);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
