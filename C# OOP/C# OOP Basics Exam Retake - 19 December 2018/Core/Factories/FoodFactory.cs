using SoftUniRestaurant.Core.Factories.Contracts;
using SoftUniRestaurant.Models.Foods;
using SoftUniRestaurant.Models.Foods.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftUniRestaurant.Core.Factories
{
    public class FoodFactory : IFoodFactory
    {
        public IFood CreateFood(string foodType, string name, decimal price)
        {
            //var type = Assembly
            //    .GetCallingAssembly()
            //    .GetTypes()
            //    .FirstOrDefault(t => t.Name == foodType);
            //IFood food = (IFood)Activator.CreateInstance(type,name,price);
            //return food;
            IFood food = null;

            switch (foodType)
            {
                case "Dessert":
                    food = new Dessert(name, price);
                    break;

                case "MainCourse":
                    food = new MainCourse(name, price);
                    break;

                case "Salad":
                    food = new Salad(name, price);
                    break;

                case "Soup":
                    food = new Soup(name, price);
                    break;

                default:
                    break;
            }
            return food;
        }
    }
}
