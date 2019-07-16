using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Animals.Contracts;
using WildFarm.Foods;

namespace WildFarm.Animals
{
    public abstract class Animal : IAnimal
    {
        private string name;
        private double weight;
        private int foodEaten;

        public Animal(string name,double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public int FoodEaten
        {
            get { return foodEaten; }
            set { foodEaten = value; }
        }

        public abstract void ProduceSound();
        public abstract void Eat(Food food);
        public override string ToString()
        {
            return $"{this.GetType().Name} [{this.Name}, ";
        }
    }
}
