using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Foods;

namespace WildFarm.Animals.Felines
{
    public class Tiger : Feline
    {
        private const double foodIncrease = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
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
               Console.WriteLine($"Tiger does not eat { food.GetType().Name}!");
            }
        }

        public override void ProduceSound()
        {
            Console.WriteLine("ROAR!!!");
        }
    }
}
