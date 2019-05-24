namespace _12.Google
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Car
    {
        public Car(string carModel,int carSpeed)
        {
            this.CarModel = carModel;
            this.CarSpeed = carSpeed;
        }
        public string CarModel { get; set; }
        public int CarSpeed { get; set; }

        public override string ToString()
        {
            return $"{this.CarModel} {this.CarSpeed}";
        }
    }
}
