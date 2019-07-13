using ExplicitInterfaces.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplicitInterfaces.Models
{
    public class Citizen : IPerson,IResident
    {
        private string name;
        private int age;
        private string country;

        public Citizen(string name, int age, string country)
        {
            this.Name = name;
            this.Age = age;
            this.Country = country;
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
        public string Country
        {
            get => country;
            private set => country = value;
        }
        string IPerson.GetName()
        {
            return $"{this.Name}";
        }
        string IResident.GetName()
        {
            return $"Mr/Ms/Mrs {this.Name}";
        }
    }
}
