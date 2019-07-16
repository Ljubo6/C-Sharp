using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Exceptions
{
    public class FoodException : ArgumentException
    {
        public FoodException(string message) 
            : base(message)
        {

        }
    }
}
