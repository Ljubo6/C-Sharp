namespace CarManufacturer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Tire
    {
        private int year;
        private double pressure;

        public Tire(int yaer,double pressure)
        {
            this.Year = year;
            this.Pressure = pressure;
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public double Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }

    }
}
