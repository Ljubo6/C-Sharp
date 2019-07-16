using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Foods.Contracts;

namespace WildFarm.Foods
{
    public abstract class Food : IFood
    {
        private int quantity;
        public Food(int quantity)
        {
            this.Quantity = quantity;
        }

        public int Quantity
        {
            //TODO Add validation
            get { return quantity; }
            set { quantity = value; }
        }

    }
}
