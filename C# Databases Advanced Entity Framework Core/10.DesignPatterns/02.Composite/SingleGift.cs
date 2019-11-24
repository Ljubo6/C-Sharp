using System;
using System.Collections.Generic;
using System.Text;

namespace _02.Composite
{
    public class SingleGift : GiftBase
    {
        public SingleGift(string name, int price) 
            : base(name, price)
        {
        }

        public override int CalculateTotalPrice()
        {
            Console.WriteLine($"{this.name} with the price {this.price}");

            return this.price;
        }
    }
}
