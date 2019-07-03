using Animals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public class Engine
    {
        private AnimalFactory animalFactory;
        private List<Animal> animals;
        public Engine()
        {
            this.animalFactory = new AnimalFactory();
            this.animals = new List<Animal>();
            
        }
        public void Run()
        {
            string input = Console.ReadLine();
            while (input != "Beast!")
            {
                try
                {
                    string type = input;
                    string[] data = Console.ReadLine().Split();

                    string name = data[0];
                    int age = int.Parse(data[1]);
                    string gender = data[2];

                    Animal animal = animalFactory.CreateAnimal(type, name, age, gender);
                    animals.Add(animal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                input = Console.ReadLine();
            }
            Print();
        }

        private void Print()
        {
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.GetType().Name);
                Console.WriteLine(animal);
            }
        }
    }
}
