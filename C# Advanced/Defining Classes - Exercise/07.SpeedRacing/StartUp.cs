namespace _07.SpeedRacing
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    public class StartUp
    {
        static List<Car> cars;
        public static void Main(string[] args)
        {
            cars = new List<Car>();
            int n = int.Parse(Console.ReadLine());
           
            for (int i = 0; i < n; i++)
            {
 
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string model = tokens[0];
                double fuelAmount = double.Parse(tokens[1]);
                double fuelConsumptionPer1Km = double.Parse(tokens[2]);
                Car car = new Car(model, fuelAmount, fuelConsumptionPer1Km);

                cars.Add(car);
            }

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = tokens[0];
                string model = tokens[1];
                double amountOfKm = double.Parse(tokens[2]);

                bool isCanMoveCar = IsCamMoveCar(model,amountOfKm);
                if (!isCanMoveCar)
                {
                    Console.WriteLine("Insufficient fuel for the drive");
                }

            }

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model} {car.FuelAmount:F2} {car.DistanceTraveled}");
            }
        }

        private static bool IsCamMoveCar(string model, double amountOfKm)
        {
            bool canMove = false;

            foreach (var car in cars)
            {
                if (car.Model == model)
                {
                    if (car.FuelAmount >= car.FuelConsumptionPer1Km * amountOfKm)
                    {
                        double consumedFuel = car.FuelConsumptionPer1Km * amountOfKm;
                        car.FuelAmount -= consumedFuel;
                        car.DistanceTraveled += amountOfKm;
                        canMove = true;

                    }

                }
            }
            return canMove;
        }
    }
}
