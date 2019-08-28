using SpaceStation.Core.Contracts;
using SpaceStation.Models;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private List<IAstronaut> astronauts;
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private Mission mission;
        private int exploredPlanetCount;
        public Controller()
        {
            this.astronauts = new List<IAstronaut>();
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
            this.mission = new Mission();
            this.exploredPlanetCount = 0;

        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;
            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            this.astronautRepository.Add(astronaut);
            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);

            for (int i = 0; i < items.Length; i++)
            {
                planet.Items.Add(items[i]);
            }
            this.planetRepository.Add(planet);
            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            var planet = this.planetRepository.FindByName(planetName);
            astronauts = this.astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();
            if (astronauts.Count() == 0)
            {

                throw new InvalidOperationException("You need at least one astronaut to explore the planet");
            }
            this.mission.Explore(planet,astronauts);
            exploredPlanetCount++;
            int deadAstronauts = astronauts.Where(x => x.CanBreath == false).ToList().Count();
            return $"Planet: {planetName} was explored! Exploration finished with {deadAstronauts} dead astronauts!";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanetCount} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astronaut in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                if (astronaut.Bag.Items.Count == 0)
                {
                    sb.AppendLine("Bag items: none");
                }
                else
                {
                    sb.AppendLine($"Bag items: {string.Join(", ",astronaut.Bag.Items)}");
                }
            }
            return sb.ToString().TrimEnd();
            
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronaut = this.astronautRepository.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }
            this.astronautRepository.Remove(astronaut);
            return $"Astronaut {astronautName} was retired!";
        }
    }
}
