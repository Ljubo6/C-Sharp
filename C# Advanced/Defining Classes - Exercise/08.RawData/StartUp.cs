using System.Collections.Generic;
using System;
using System.Linq;

namespace _08.RawData
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split();

                string model = input[0];
                int engineSpeed = int.Parse(input[1]);
                int enginePower = int.Parse(input[2]);
                int cargoWeight = int.Parse(input[3]);
                string cargoType = input[4];
                List<Tire> tires = new List<Tire>();

                for (int j = 0; j < 4; j+= 2)
                {
                    double tirePressure = double.Parse(input[5 + j]);
                    int tireAge = int.Parse(input[6 + j]);

                    Tire tire = new Tire(tireAge,tirePressure);
                    tires.Add(tire);
                }

                Engine engine = new Engine(engineSpeed,enginePower);
                Cargo cargo = new Cargo(cargoWeight,cargoType);

                Car car = new Car(model,engine,cargo,tires);
                cars.Add(car);
            }
            string command = Console.ReadLine();
            List<Car> resultCars = new List<Car>();
            if (command == "fragile")
            {
                resultCars = cars
                    .Where(x => x.Cargo.CargoType == "fragile" && x.Tires.Any(s => s.Pressure < 1))
                    .ToList();
            }
            else
            {
                resultCars = cars
                    .Where(x => x.Cargo.CargoType == "flamable" && x.Engine.EnginePower > 250)
                    .ToList();
            }
            foreach (var car in resultCars)
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}
