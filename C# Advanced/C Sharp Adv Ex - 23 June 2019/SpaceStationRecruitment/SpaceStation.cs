using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStationRecruitment
{
    public class SpaceStation
    {
        private List<Astronaut> data;
        public SpaceStation(string name,int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.data = new List<Astronaut>();
        }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Count => this.data.Count;

        public void Add(Astronaut astronaut)
        {
            if (this.data.Count < this.Capacity)
            {
                this.data.Add(astronaut);
            }
            
        }
        public bool Remove(string name)
        {
            var currentAstonaut = data.FirstOrDefault(x => x.Name == name);
            if (currentAstonaut == null)
            {
                return false;
            }
            else
            {
                data.Remove(currentAstonaut);
                return true;
            }
        }
        public Astronaut GetOldestAstronaut()
        {
            return data.OrderByDescending(x => x.Age).FirstOrDefault();
        }
        public Astronaut GetAstronaut(string name)
        {
            return data.OrderByDescending(x => x.Name == name).FirstOrDefault();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Astronauts working at Space Station {this.Name}:");
            sb.Append(string.Join(Environment.NewLine, this.data));
            return sb.ToString();
        }

    }
}
