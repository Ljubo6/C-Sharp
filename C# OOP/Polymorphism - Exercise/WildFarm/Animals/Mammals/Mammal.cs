using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Animals.Mammals
{
    public abstract class Mammal : Animal
    {
        private string livingRegion;
        public Mammal(string name, double weight,string livingRegion) 
            : base(name, weight)
        {
            this.Livingregion = livingRegion;
        }

        public string Livingregion
        {
            get { return livingRegion; }
            set { livingRegion = value; }
        }
        //public override string ToString()
        //{
        //    return base.ToString() + $"{this.Weight}, {this.Livingregion}, {this.FoodEaten}]";
        //}
    }
}
