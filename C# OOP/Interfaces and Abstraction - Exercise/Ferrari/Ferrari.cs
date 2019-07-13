using System;
using System.Collections.Generic;
using System.Text;

namespace Ferrari
{
    public class Ferrari : IFerrari
    {
        public Ferrari(string driverName)
        {
            this.Driver = driverName;
        }
        public string Model => "488-Spider";

        public string Driver { get; private set; }

        public string Gas()
        {
            return "Gas!";
        }

        public string PushBrakes()
        {
            return "Brakes!";
        }
        public override string ToString()
        {
            return $"{this.Model}/{this.PushBrakes()}/{this.Gas()}/{this.Driver}";
        }
    }

}
