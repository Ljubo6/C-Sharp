using MortalEngines.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Core
{
    public class Engine
    {
        private IMachinesManager manager;
        public Engine()
        {
            this.manager = new MachinesManager();
        }

        public void Run()
        {
            string line = string.Empty;
            while ((line = Console.ReadLine()) != "Quit")
            {
                string[] commandItems = line.Split();
                string command = commandItems[0];
                string result = string.Empty;

                try
                {
                    string name = commandItems[1];
                    switch (command)
                    {

                        case "HirePilot":

                            result += this.manager.HirePilot(name);
                            break;
                        case "PilotReport":
                            result += this.manager.PilotReport(name);
                            break;
                        case "ManufactureTank":
                            double attack = double.Parse(commandItems[2]);
                            double defense = double.Parse(commandItems[3]);
                            result += this.manager.ManufactureTank(name, attack, defense);
                            break;
                        case "ManufactureFighter":
                            attack = double.Parse(commandItems[2]);
                            defense = double.Parse(commandItems[3]);
                            result += this.manager.ManufactureFighter(name, attack, defense);
                            break;
                        case "MachineReport":
                            result += this.manager.MachineReport(name);
                            break;
                        case "AggressiveMode":
                            result += this.manager.ToggleFighterAggressiveMode(name);
                            break;
                        case "DefenseMode":
                            result += this.manager.ToggleTankDefenseMode(name);
                            break;
                        case "Engage":
                            string pilotName = commandItems[1];
                            string machineName = commandItems[2];
                            result += this.manager.EngageMachine(pilotName, machineName);
                            break;
                        case "Attack":
                            string attackingMachineName = commandItems[1];
                            string defendingMachineName = commandItems[2];
                            result += this.manager.AttackMachines(attackingMachineName, defendingMachineName);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(result);
            }
        }
    }
}
