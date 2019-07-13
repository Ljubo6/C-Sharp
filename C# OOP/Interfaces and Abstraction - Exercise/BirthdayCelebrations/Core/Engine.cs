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
        private List<IIdentifiable> creatures;
        private List<IBirthable> birthdates;
        public Engine()
        {
            this.creatures = new List<IIdentifiable>();
            this.birthdates = new List<IBirthable>();
        }
        public void Run()
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] inputArgs = input.Split();

                string type = inputArgs[0];
                if (type == "Robot")
                {
                    string model = inputArgs[1];
                    string id = inputArgs[2];

                    IIdentifiable robot = new Robot(model,id);

                    this.creatures.Add(robot);
                }
                else if(type == "Citizen")
                {
                    string name = inputArgs[1];
                    int age = int.Parse(inputArgs[2]);
                    string id = inputArgs[3];
                    string birthdate = inputArgs[4];
                    IBirthable citizen = new Citizen(name,age,id,birthdate);
                    this.birthdates.Add(citizen);
                }
                else if (type == "Pet")
                {
                    string name = inputArgs[1];
                    string birthdate = inputArgs[2];

                    IBirthable pet = new Pet(name,birthdate);
                    this.birthdates.Add(pet);
                }
            }
            string year = Console.ReadLine();
            foreach (var item in this.birthdates.Where(x => x.Birthdate.EndsWith(year)))
            {
                Console.WriteLine(item.Birthdate);
            }

        }
    }
}
