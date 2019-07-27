﻿using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Fighter : BaseMachine , IFighter
    {
        private const int InitialHealthPoints = 200;
        private const int IncreaseAttackPoints = 50;
        private const int DecreaseDeffencePoints = 25;

        public Fighter(string name, double attackPoints, double defensePoints) 
            : base(name, attackPoints + IncreaseAttackPoints, defensePoints - DecreaseDeffencePoints, InitialHealthPoints)
        {
            this.AggressiveMode = true;

        }

        public bool AggressiveMode { get; private set; }

        public void ToggleAggressiveMode()
        {
            if (this.AggressiveMode == false)
            {
                this.AttackPoints += IncreaseAttackPoints;
                this.DefensePoints -= DecreaseDeffencePoints;
            }
            else
            {
                this.AttackPoints -= IncreaseAttackPoints;
                this.DefensePoints += DecreaseDeffencePoints;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine (base.ToString());
            sb.AppendLine($" *Aggressive: {(this.AggressiveMode == true ? "ON" : "OFF")}");
            return sb.ToString().TrimEnd();
        }
    }
}
