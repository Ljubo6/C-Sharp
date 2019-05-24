namespace _5.SpecialCars
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Car
    {
        public Car(string make,string model,int year,
            double fuelQuantity,double fuelConsumption,
            Engine engine,Tire tire)
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
            this.Engine = engine;
            this.Tire = tire;
        }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double FuelQuantity { get; set; }
        public double FuelConsumption { get; set; }
        public Engine Engine { get; set; }
        public Tire Tire { get; set; }
    }
}
