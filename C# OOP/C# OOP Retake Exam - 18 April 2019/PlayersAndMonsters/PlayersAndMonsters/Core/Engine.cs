using PlayersAndMonsters.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Core
{
    public class Engine : IEngine
    {
        private readonly IManagerController managerController;
        public Engine()
        {
            this.managerController = new ManagerController();
        }
        public void Run()
        {
            string line = string.Empty;
            while ((line = Console.ReadLine()) != "Exit")
            {
                string[] commandItems = line.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string result = string.Empty;
                string command = commandItems[0];
                try
                {
                    switch (command)
                    {
                        case "AddPlayer":
                            result += this.managerController.AddPlayer(commandItems[1],commandItems[2]);
                            break;
                        case "AddCard":
                            result += this.managerController.AddCard(commandItems[1], commandItems[2]);
                            break;
                        case "AddPlayerCard":
                            result += this.managerController.AddPlayerCard(commandItems[1], commandItems[2]);
                            break;
                        case "Fight":
                            result += this.managerController.Fight(commandItems[1], commandItems[2]);
                            break;
                        case "Report":
                            result += this.managerController.Report();
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
