namespace _12.Google
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string input = string.Empty;
            List<Person> persons = new List<Person>();
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                string command = tokens[1];
                Person person = new Person(name);
                if (!persons.Any(x => x.Name == name))
                {
                    persons.Add(person);
                }

                person = persons.Where(x => x.Name == name).First();
                if (command == "company")
                {
                    string companyName = tokens[2];
                    string department = tokens[3];
                    decimal salary = decimal.Parse(tokens[4]);
                    person.Company = new Company(companyName, department, salary);
                }
                else if (command == "pokemon")
                {
                    string pokemonName = tokens[2];
                    string pokemonType = tokens[3];
                    person.Pokemons.Add(new Pokemon(pokemonName, pokemonType));
                }
                else if (command == "parents")
                {
                    string parentName = tokens[2];
                    string parentBirthday = tokens[3];
                    person.Parents.Add(new Parent(parentName, parentBirthday));
                }
                else if (command == "children")
                {
                    string childName = tokens[2];
                    string childBirthday = tokens[3];
                    person.Children.Add(new Child(childName, childBirthday));
                }
                else if (command == "car")
                {
                    string carModel = tokens[2];
                    int carSpeed = int.Parse(tokens[3]);
                    person.Car = new Car(carModel,carSpeed);
                }
            }
            string singleName = Console.ReadLine();

            var per = persons.Where(x => x.Name == singleName).FirstOrDefault();

            Console.WriteLine(per);

        }
    }
}
