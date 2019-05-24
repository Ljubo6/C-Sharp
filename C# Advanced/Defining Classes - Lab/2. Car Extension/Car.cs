namespace CarManufacturer
{
    using System;
    using System.Text;

    public class Car
    {
        private string make;
        private string model;
        private int year;
        private double fuelQuantity;
        private double fuelConsumption;

        public string Make
        {
            get { return make; }
            set { make = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
       
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public double FuelQuantity
        {
            get { return fuelQuantity; }
            set { fuelQuantity = value; }
        }

        public double FuelConsumption
        {
            get { return fuelConsumption; }
            set { fuelConsumption = value; }
        }

        public void Drive(double distance)
        {
            bool canContinue = this.FuelQuantity - (distance * this.FuelConsumption) / 100 >= 0;
            if (canContinue)
            {
                this.FuelQuantity -= (distance * this.FuelConsumption) / 100 ;
            }
            else
            {
               throw new InvalidOperationException("Not enough fuel to perform this trip!");
            }
        }
        public string WhoAmI()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Make: {this.Make}");

            sb.AppendLine($"Model: {this.Model}");

            sb.AppendLine($"Year: {this.Year}");

            sb.Append($"Fuel: {this.FuelQuantity:F2}L");

            return sb.ToString();
        }
    }
}
