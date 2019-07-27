﻿namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Common;
    using MortalEngines.Entities;
    using MortalEngines.Entities.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private readonly List<IPilot> pilots;
        private readonly List<IMachine> machines;
        public MachinesManager()
        {
            this.pilots = new List<IPilot>() ;
            this.machines = new List<IMachine>();
        }
        public string HirePilot(string name)
        {
            if (this.pilots.Any(p => p.Name == name))
            {
                return string.Format(OutputMessages.PilotExists,name);
            }
            else
            {
                IPilot pilot = new Pilot(name);
                this.pilots.Add(pilot);
                return string.Format(OutputMessages.PilotHired,name);
            }
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            if (this.machines.Any(m => m.Name == name && m.GetType().Name == nameof(Tank)))
            {
                return string.Format(OutputMessages.MachineExists,name);
            }
            else
            {
                ITank tank = new Tank(name, attackPoints, defensePoints);
                this.machines.Add(tank);
                return string.Format(OutputMessages.TankManufactured,
                    tank.Name,
                    tank.AttackPoints,
                    tank.DefensePoints);
            }
            
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            if (this.machines.Any(f => f.Name == name && f.GetType().Name == nameof(Fighter)))
            {
                return string.Format(OutputMessages.MachineExists,name);
            }
            else
            {
                IFighter fighter = new Fighter(name,attackPoints,defensePoints);
                this.machines.Add(fighter);
                return string.Format(OutputMessages.FighterManufactured,
                    fighter.Name,
                    fighter.AttackPoints,
                    fighter.DefensePoints,
                    fighter.AggressiveMode == true ? "ON" : "OFF");
            }
        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            IPilot pilot = this.pilots.FirstOrDefault(p => p.Name == selectedPilotName);
            if (pilot == null)
            {
                return string.Format(OutputMessages.PilotNotFound,selectedPilotName);
            }
            IMachine machine = this.machines.FirstOrDefault(m => m.Name == selectedMachineName);
            if (machine == null)
            {
                return string.Format(OutputMessages.MachineNotFound,selectedMachineName);
            }
            if (machine.Pilot != null)
            {
                return string.Format(OutputMessages.MachineHasPilotAlready,selectedMachineName);
            }

            pilot.AddMachine(machine);
            machine.Pilot = pilot;
            return string.Format(OutputMessages.MachineEngaged,pilot.Name,machine.Name);
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            IMachine attacker = this.machines.FirstOrDefault(a => a.Name == attackingMachineName);
            if (attacker == null)
            {
                return string.Format(OutputMessages.MachineNotFound,attackingMachineName);
            }
            IMachine defender = this.machines.FirstOrDefault(d => d.Name == defendingMachineName);
            if (defender == null)
            {
                return string.Format(OutputMessages.MachineNotFound,defendingMachineName);
            }
            if (attacker.HealthPoints <= 0)
            {
                return string.Format(OutputMessages.DeadMachineCannotAttack,attackingMachineName);
            }
            if (defender.HealthPoints <= 0)
            {
                return string.Format(OutputMessages.DeadMachineCannotAttack,defendingMachineName);
            }
            attacker.Attack(defender);
            return string.Format(OutputMessages.AttackSuccessful,defender.Name,attacker.Name,defender.HealthPoints);
        }

        public string PilotReport(string pilotReporting)
        {
            return this.pilots.FirstOrDefault(p => p.Name == pilotReporting).Report();
        }

        public string MachineReport(string machineName)
        {
            return this.machines.FirstOrDefault(m => m.Name == machineName).ToString();
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            IFighter fighter = (Fighter)this.machines
                .FirstOrDefault(f => f.Name == fighterName && f.GetType().Name == nameof(Fighter));
            if (fighter == null)
            {
                return string.Format(OutputMessages.MachineNotFound,fighterName);
            }
            else
            {
                fighter.ToggleAggressiveMode();
                return string.Format(OutputMessages.FighterOperationSuccessful,fighter.Name);
            }
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            ITank tank = (Tank)this.machines
                .FirstOrDefault(t => t.Name == tankName && t.GetType().Name == nameof(Tank));
            if (tank == null)
            {
                return string.Format(OutputMessages.MachineNotFound,tankName);
            }
            else
            {
                tank.ToggleDefenseMode();
                return string.Format(OutputMessages.TankOperationSuccessful,tank.Name);
            }
        }
    }
}