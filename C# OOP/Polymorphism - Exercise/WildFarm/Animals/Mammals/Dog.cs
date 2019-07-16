using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Foods;

namespace WildFarm.Animals.Mammals
{
    public class Dog : Mammal
    {
        private const double foodIncrease = 0.40;
        public Dog(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion)
        {
        }

        public override void Eat(Food food)
        {
            if (food is Meat)
            {
                this.Weight += food.Quantity * foodIncrease;
                this.FoodEaten += food.Quantity;
            }
            else
            {
               Console.WriteLine($"Dog does not eat { food.GetType().Name}!");
            }
        }

        public override void ProduceSound()
        {
            Console.WriteLine("Woof!");
        }
        public override string ToString()
        {
            return base.ToString() + $"{this.Weight}, {this.Livingregion}, {this.FoodEaten}]";
        }
    }
}
