namespace FightingArena
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Gladiator
    {
        public Gladiator(string name, Stat stat, Weapon weapon)
        {
            this.Name = name;
            this.Stat = stat;
            this.Weapon = weapon;
        }
        public string Name { get; set; }
        public Stat Stat { get; set; }
        public Weapon Weapon { get; set; }

        public int GetTotalPower()
        {
            int weaponPower = GetWeaponPower();
            int statPower = GetStatPower();
            return weaponPower + statPower;
        }
        public int GetWeaponPower()
        {
            return this.Weapon.Size
                + this.Weapon.Solidity
                + this.Weapon.Sharpness;
        }
        public int GetStatPower()
        {
            return this.Stat.Strength 
                + this.Stat.Flexibility 
                + this.Stat.Agility 
                + this.Stat.Skills 
                + this.Stat.Intelligence;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{this.Name}] - [{GetTotalPower()}]");
            sb.AppendLine($"  Weapon Power: [{GetWeaponPower()}]");
            sb.Append($"  Stat Power: [{GetStatPower()}]");
            return sb.ToString();
        }
    }
}
