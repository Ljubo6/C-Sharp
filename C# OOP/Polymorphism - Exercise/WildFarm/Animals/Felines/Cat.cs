using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Foods;

namespace WildFarm.Animals.Felines
{
    public class Cat : Feline
    {
        private const double foodIncrease = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eat(Food food)
        {
            if (food is Vegetable || food is Meat)
            {
                this.Weight += food.Quantity * foodIncrease;
                this.FoodEaten += food.Quantity;
            }
            else
            {
               Console.WriteLine($"Cat does not eat { food.GetType().Name}!");
            }
        }

        public override void ProduceSound()
        {
            Console.WriteLine("Meow");
        }
    }
}
