using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Animals.Mammals.Factory
{
    public class MammalFactory
    {
        public Mammal CreateMammal(string type,string name,double weight,string livingRegion)
        {
            type = type.ToLower();
            switch (type)
            {
                case "mouse":
                    return new Mouse(name,weight,livingRegion);
                case "dog":
                    return new Dog(name, weight, livingRegion);
                default:
                    return null;
            }
        }
    }
}
