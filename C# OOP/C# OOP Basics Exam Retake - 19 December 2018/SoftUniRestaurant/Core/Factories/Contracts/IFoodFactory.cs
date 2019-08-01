using SoftUniRestaurant.Models.Foods.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Core.Factories.Contracts
{
    public interface IFoodFactory
    {
        IFood CreateFood(string type,string name,decimal price);
    }
}
