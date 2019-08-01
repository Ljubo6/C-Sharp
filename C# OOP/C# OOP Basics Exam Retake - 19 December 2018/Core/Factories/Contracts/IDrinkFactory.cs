using SoftUniRestaurant.Models.Drinks.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Core.Factories.Contracts
{
    public interface IDrinkFactory
    {
        IDrink CreateDrink(string type,string name,int servingSize,string brand);
    }
}
