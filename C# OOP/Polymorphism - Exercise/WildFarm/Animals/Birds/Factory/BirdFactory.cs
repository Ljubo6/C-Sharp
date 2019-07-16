using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Animals.Birds.Factory
{
    public class BirdFactory
    {
        public Bird CreateBird(string type,string name,double weight,double wingSize)
        {
            type = type.ToLower();
            switch (type)
            {
                case "hen":
                    return new Hen(name,weight,wingSize);
                case "owl":
                    return new Owl(name, weight, wingSize);
                default:
                    return null;
            }
        }
    }
}
