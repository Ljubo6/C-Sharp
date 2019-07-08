using FootballTeamGenerator.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator.Models
{
    public class Player
    {
        private string  name;
        public Player(string name,Stat stat)
        {
            this.Name = name;
            this.Stat = stat;
        }
        public string  Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.EmptyNameException);


                }
                this.name = value;
            }
        }
        public double OverallSkill => this.Stat.OverallStat();
        public Stat Stat { get; private set; }

       
    }
}
