using FoodShortage.Contracts;
using FoodShortage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodShortage.Core
{
    public class Engine
    {
        private List<IBuyer> people;
        public Engine()
        {
            this.people = new List<IBuyer>();
        }
        public void Run()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] inputArgs = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);

                if(inputArgs.Length == 4)
                {
                    string name = inputArgs[0];
                    int age = int.Parse(inputArgs[1]);
                    string id = inputArgs[2];
                    string birthdate = inputArgs[3];
                    IBuyer citizen = new Citizen(name,age,id,birthdate);
                    this.people.Add(citizen);
                }
                else
                {
                    string name = inputArgs[0];
                    int age = int.Parse(inputArgs[1]);
                    string group = inputArgs[2];
                    IBuyer rebel = new Rebel(name,age,group);
                    this.people.Add(rebel);
                }
            }
            string person = string.Empty;
            while ((person = Console.ReadLine()) != "End")
            {
                if (people.FirstOrDefault(x => x.Name == person) != null)
                {
                    people.FirstOrDefault(x => x.Name == person).BuyFood();
                }
            }
            Console.WriteLine(people.Sum(x => x.Food));
        }
    }
}
