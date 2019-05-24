namespace _5.SpecialCars
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Engine
    {
        public Engine(int horsePower,double cubicCapacity)
        {
            this.HorsePower = horsePower;
            this.CubicCapacity = cubicCapacity;
        }
        public int HorsePower { get; set; }
        public double CubicCapacity { get; set; }
    }
}
