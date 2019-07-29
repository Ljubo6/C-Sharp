using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace AnimalCentre.Models
{
    public class Hotel : IHotel
    {
        private const int Capacity = 10;
        private readonly Dictionary<string, IAnimal> animals;
        public Hotel()
        {
            this.animals = new Dictionary<string,IAnimal>();
        }
        public IReadOnlyDictionary<string, IAnimal> Animals => this.animals.ToImmutableDictionary();

        public void Accommodate(IAnimal animal)
        {
            if (animals.Count == Capacity)
            {
                throw new InvalidOperationException("Not enough capacity");
            }
            if (!Animals.ContainsKey(animal.Name))
            {
                this.animals[animal.Name] =  animal;
            }
            else
            {
                throw new ArgumentException($"Animal {animal.Name} already exist");
            }
            
        }

        public void Adopt(string animalName, string owner)
        {
            if (!animals.ContainsKey(animalName))
            {
                throw new ArgumentException($"Animal {animalName} does not exist");
            }
            animals[animalName].Owner = owner;
            animals[animalName].IsAdopt = true;
            animals.Remove(animalName);

        }
    }
}
