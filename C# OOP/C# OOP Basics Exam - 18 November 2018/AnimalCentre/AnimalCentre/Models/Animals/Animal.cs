using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        private string name;
        private int energy;
        private int happiness;
        private int procedureTime;
        private string defaultOwner = "Centre";
        protected Animal(string name, int energy, int happiness, int procedureTime)
        {
            this.Name = name;
            this.Energy = energy;
            this.Happiness = happiness;
            this.ProcedureTime = procedureTime;
            this.Owner = defaultOwner;
            this.IsAdopt = false;
            this.IsChipped = false;
            this.IsVaccinated = false;
        }
        public string Name { get; }
        public int Happiness
        {
            get
            {
                return this.happiness;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Invalid happiness");
                }
                this.happiness = value;
            }
        }
        public int Energy
        {
            get
            {
                return this.energy;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Invalid energy");
                }
                this.energy = value;
            }
        }
        public int ProcedureTime { get; set; }
        public string Owner { get; set; }
        public bool IsAdopt { get; set; }
        public bool IsChipped { get;set; }
        public bool IsVaccinated { get;set; }
    }
}
