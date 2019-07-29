using AnimalCentre.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre.Core
{
    public class Engine : IEngine
    {
        private AnimalCentre animalCenter;
        public Engine()
        {
            this.animalCenter = new AnimalCentre();
        }
        public void Run()
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] inputArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string command = inputArgs[0];                    
                    string[] args = inputArgs.Skip(1).ToArray();
                    string result = ReadCommand(command,args);
                    Console.WriteLine(result);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine("InvalidOperationException: " + ioe.Message);
                }
                catch(ArgumentException ae)
                {
                    Console.WriteLine("ArgumentException: " + ae.Message);
                }
            }
            string adoptedAnimals = this.animalCenter.AllAdoptedAnimals();
            Console.WriteLine(adoptedAnimals);
        }

        private string ReadCommand(string command, string[] args)
        {
            string name = string.Empty;
            int procedureTime = 0;
            var output = String.Empty;
            switch (command)
            {
                case "RegisterAnimal":

                    var type = args[0];
                    name = args[1];
                    var energy = int.Parse(args[2]);
                    var happiness = int.Parse(args[3]);
                    procedureTime = int.Parse(args[4]);
                    output = this.animalCenter.RegisterAnimal(type, name, energy, happiness, procedureTime);
                    break;
                case "Chip":
                    name = args[0];
                    procedureTime = int.Parse(args[1]);
                    output = this.animalCenter.Chip(name, procedureTime);
                    break;
                case "Play":
                    name = args[0];
                    procedureTime = int.Parse(args[1]);
                    output = this.animalCenter.Play(name, procedureTime);
                    break;
                case "Fitness":
                    name = args[0];
                    procedureTime = int.Parse(args[1]);
                    output = this.animalCenter.Fitness(name, procedureTime);
                    break;
                case "NailTrim":
                    name = args[0];
                    procedureTime = int.Parse(args[1]);
                    output = this.animalCenter.NailTrim(name, procedureTime);
                    break;
                case "Vaccinate":
                    name = args[0];
                    procedureTime = int.Parse(args[1]);
                    output = this.animalCenter.Vaccinate(name, procedureTime);
                    break;
                case "DentalCare":
                    name = args[0];
                    procedureTime = int.Parse(args[1]);
                    output = this.animalCenter.DentalCare(name, procedureTime);
                    break;
                case "Adopt":
                    name = args[0];
                    string owner = args[1];
                    output = this.animalCenter.Adopt(name, owner);
                    break;
                case "History":
                    name = args[0];
                    output = this.animalCenter.History(name);
                    break;
            }
            return output;
        }
    }
}
