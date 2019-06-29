namespace P01_RawData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class StartUp
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            int lines = int.Parse(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                string[] parameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string model = parameters[0];
                int speed = int.Parse(parameters[1]);
                int power = int.Parse(parameters[2]);
                int weight = int.Parse(parameters[3]);
                string type = parameters[4];
                                
                Engine engine = new Engine(speed, power);
                Cargo cargo = new Cargo(weight, type);
                Tire[] tires = new Tire[4];

                int counterTire = 0;
                for (int j = 5; j <= 12; j+=2)
                {
                    double pressure = double.Parse(parameters[j]);
                    int age = int.Parse(parameters[j + 1]);
                    Tire tire = new Tire(pressure,age);
                    tires[counterTire] = tire;
                    counterTire++;
                }
                Car car = new Car(model,engine,cargo,tires);
                cars.Add(car);
            }

            string command = Console.ReadLine().ToLower();
            List<Car> sortedCars = new List<Car>();
            if (command == "fragile")
            {
                sortedCars = cars
                    .Where(x => x.Cargo.Type == "fragile" && x.Tires.Any(y => y.Pressure < 1))
                    .ToList();
            }
            else
            {
               sortedCars = cars
                    .Where(x => x.Cargo.Type == "flamable" && x.Engine.Power > 250)
                    .ToList();
            }
            Console.WriteLine(string.Join(Environment.NewLine, sortedCars));
        }
    }
}
