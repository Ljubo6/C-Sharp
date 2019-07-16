using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Foods;

namespace WildFarm.Animals.Mammals
{
    public class Mouse : Mammal
    {
        private const double foodIncrease = 0.10;

        public Mouse(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion)
        {
        }

        public override void Eat(Food food)
        {
            if (food is Vegetable || food is Fruit)
            {
                this.Weight += food.Quantity * foodIncrease;
                this.FoodEaten += food.Quantity;
            }
            else
            {
               Console.WriteLine($"Mouse does not eat { food.GetType().Name}!");
            }
        }

        public override void ProduceSound()
        {
            Console.WriteLine("Squeak");
        }
        public override string ToString()
        {
            return base.ToString() + $"{this.Weight}, {this.Livingregion}, {this.FoodEaten}]";
        }
    }
}
