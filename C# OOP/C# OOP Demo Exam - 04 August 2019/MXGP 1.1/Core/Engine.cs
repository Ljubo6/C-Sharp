using MXGP.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Core
{
    public class Engine : IEngine
    {
        private const string EndCommand = "End";
        private IChampionshipController championshipController;

        public Engine()
        {
            this.championshipController = new ChampionshipController();
        }
        public void Run()
        {

            string input = string.Empty;
            while ((input = Console.ReadLine()) != EndCommand)
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                string[] args = tokens.Skip(1).ToArray();
                try
                {
                    string result = ReadCommand(command, args);
                    Console.WriteLine(result);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch(InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }

            }
        }

        private string ReadCommand(string command, string[] args)
        {
            string output = string.Empty;
            switch (command)
            {
                case "CreateRider":
                    string name = args[0];
                    output = championshipController.CreateRider(name);
                    break;
                case "CreateMotorcycle":
                    string motorcycleType = args[0];
                    string model = args[1];
                    int horsepower = int.Parse(args[2]);
                    output = championshipController.CreateMotorcycle(motorcycleType,model,horsepower);
                    break;
                case "AddMotorcycleToRider":
                    string riderName = args[0];
                    string motorcycleName = args[1];
                    output = championshipController.AddMotorcycleToRider(riderName,motorcycleName);
                    break;
                case "AddRiderToRace":
                    string raceName = args[0];
                    riderName = args[1];
                    output = championshipController.AddRiderToRace(raceName,riderName);
                    break;
                case "CreateRace":
                    name = args[0];
                    int laps = int.Parse(args[1]);
                    output = championshipController.CreateRace(name,laps);
                    break;
                case "StartRace":
                    raceName = args[0];
                    output = championshipController.StartRace(raceName);
                    break;
                default:
                    break;
            }
            return output;
        }
    }
}
