using BorderControl.Contracts;
using BorderControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderControl.Core
{
    public class Engine
    {
        private List<IIdentifiable> creatures;
        public Engine()
        {
            this.creatures = new List<IIdentifiable>();
        }
        public void Run()
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] inputArgs = input.Split();
                if (inputArgs.Length == 2)
                {
                    string model = inputArgs[0];
                    string id = inputArgs[1];

                    IIdentifiable robot = new Robot(model,id);

                    this.creatures.Add(robot);
                }
                else
                {
                    string name = inputArgs[0];
                    int age = int.Parse(inputArgs[1]);
                    string id = inputArgs[2];

                    IIdentifiable citizen = new Citizen(name,age,id);
                    this.creatures.Add(citizen);
                }
            }
            string fakeId = Console.ReadLine();
            foreach (var item in this.creatures.Where(x => x.Id.EndsWith(fakeId)))
            {
                Console.WriteLine(item.Id);
            }
            this.creatures.RemoveAll(x => x.Id.EndsWith(fakeId));
        }
    }
}
