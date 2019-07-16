using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Animals.Mammals;

namespace WildFarm.Animals.Felines
{
    public abstract class Feline : Mammal
    {
        private string breed;
        public Feline(string name, double weight, string livingRegion,string breed) 
            : base(name, weight, livingRegion)
        {
            this.Breed = breed;
        }


        public string Breed
        {
            //TODO Add validation
            get { return breed; }
            set { breed = value; }
        }

        public override string ToString()
        {
            return base.ToString() + $"{ this.Breed}, { this.Weight}, { this.Livingregion}, { this.FoodEaten}]";
        }

    }
}
