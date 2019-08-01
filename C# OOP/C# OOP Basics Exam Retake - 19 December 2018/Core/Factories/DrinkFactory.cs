using SoftUniRestaurant.Core.Factories.Contracts;
using SoftUniRestaurant.Models.Drinks;
using SoftUniRestaurant.Models.Drinks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftUniRestaurant.Core.Factories
{
    public class DrinkFactory : IDrinkFactory
    {
        public IDrink CreateDrink(string drinkType, string name, int servingSize, string brand)
        {
            //Type type = Assembly
            //    .GetCallingAssembly()
            //    .GetTypes()
            //    .FirstOrDefault(d => d.Name == drinkType);
            //IDrink drink = (IDrink)Activator.CreateInstance(type,name,servingSize,brand);
            //return drink;
            IDrink drink = null;

            switch (drinkType)
            {
                case "Alcohol":
                    drink = new Alcohol(name, servingSize, brand);
                    break;

                case "FuzzyDrink":
                    drink = new FuzzyDrink(name, servingSize, brand);
                    break;

                case "Juice":
                    drink = new Juice(name, servingSize, brand);
                    break;

                case "Water":
                    drink = new Water(name, servingSize, brand);
                    break;

                default:
                    break;
            }
            return drink;
        }
    }
}
