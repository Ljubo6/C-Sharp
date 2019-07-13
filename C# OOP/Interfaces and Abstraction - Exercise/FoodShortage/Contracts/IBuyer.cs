using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage.Contracts
{
    public interface IBuyer
    {
        string Name { get; }
        int Age { get; }
        int Food { get; }
        void BuyFood();
    }
}
