using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {            

            foreach (var astronaut in astronauts)
            {
                if (astronaut.CanBreath == false)
                {
                    continue;
                }
                List<string> items = planet.Items.ToList();
                foreach (var item in items)
                {
                   
                    if (astronaut.CanBreath)
                    {
                        astronaut.Breath();
                        astronaut.Bag.Items.Add(item);
                        planet.Items.Remove(item);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
