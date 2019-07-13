using FoodShortage.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage.Models
{
    public class Rebel : IBuyer
    {
        private string name;
        private int age;
        private string group;
        private int food;
        public Rebel(string name,int age,string group)
        {
            this.Name = name;
            this.Age = age;
            this.Group = group;
            this.Food = 0;
        }
        public string Name
        {
            get => name;
            private set => name = value;
        }
        public int Age
        {
            get => age;
            private set => age = value;
        }
        public string Group
        {
            get => group;
            private set => group = value;
        }
        public int Food
        {
            get => food;
            private set => food = value;
        }
        public void BuyFood()
        {
            this.Food += 5;
        }
    }
}
