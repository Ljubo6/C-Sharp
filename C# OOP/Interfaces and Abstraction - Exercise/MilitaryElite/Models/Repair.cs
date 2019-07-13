using MilitaryElite.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class Repair : IRepair
    {
        private string partName;
        private int hoursWorked;
        public Repair(string partName,int hoursWorked)
        {
            this.PartName = partName;
            this.HoursWorked = hoursWorked;
        }
        public string PartName
        {
            get => partName;
            private set => partName = value;
        }
        public int HoursWorked
        {
            get => hoursWorked;
            private set => hoursWorked = value;
        }
        public override string ToString()
        {
            return $"Part Name: {this.PartName} Hours Worked: {this.HoursWorked}";
        }
    }
}
