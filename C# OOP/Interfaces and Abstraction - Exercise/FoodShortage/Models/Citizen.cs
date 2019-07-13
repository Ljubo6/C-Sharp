using FoodShortage.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage.Models
{
    public class Citizen : IIdentifiable, IBirthable,IBuyer
    {
        private string name;
        private int age;
        private string id;
        private string birthdate;
        private int food;
        public Citizen(string name,int age,string id,string birthdate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.Birthdate = birthdate;
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
        public string Id
        {
            get => id;
            private set => id = value;
        }
        public string Birthdate
        {
            get => birthdate;
            private set => birthdate = value;
        }

        public int Food
        {
            get => food;
            private set => food = value;
        }

        public void BuyFood()
        {
            this.Food += 10;
        }
    }
}
