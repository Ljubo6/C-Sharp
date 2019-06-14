namespace FightingArena
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class Arena
    {
        private List<Gladiator> gladiators;
        public Arena(string name)
        {
            this.Name = name;
            gladiators = new List<Gladiator>();
        }
        public int Count => gladiators.Count;
        public string Name { get; set; }
        public void Add(Gladiator gladiator)
        {
            gladiators.Add(gladiator);
        }
        public void Remove(string name)
        {
            var tempGladiator = gladiators.FirstOrDefault(x => x.Name == name);
            gladiators.Remove(tempGladiator);
        }
        public Gladiator GetGladitorWithHighestStatPower()
        {
            gladiators = gladiators.OrderByDescending(x => x.GetStatPower()).ToList();
            return gladiators.FirstOrDefault();
        }
        public Gladiator GetGladitorWithHighestWeaponPower()
        {
            gladiators = gladiators.OrderByDescending(x => x.GetWeaponPower()).ToList();
            return gladiators.FirstOrDefault();
        }
        public Gladiator GetGladitorWithHighestTotalPower()
        {
            gladiators = gladiators.OrderByDescending(x => x.GetTotalPower()).ToList();
            return gladiators.FirstOrDefault();
        }

        public override string ToString()
        {
            return $"[{this.Name}] - [{this.Count}] gladiators are participating.";
        }
    }
}
