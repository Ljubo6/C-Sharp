namespace _05.ComparingObjects
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            string input = string.Empty;
            int equalPeople = 0;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split();
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string town = tokens[2];

                Person person = new Person(name,age,town);
                people.Add(person);

               
            }

            int index = int.Parse(Console.ReadLine()) - 1;

            Person comparePerson = people[index];



            foreach (var p in people)
            {
                if (p.CompareTo(comparePerson) == 0)
                {
                    equalPeople++;
                }
            }
            if (equalPeople > 1)
            {
                Console.WriteLine($"{equalPeople} {people.Count - equalPeople} {people.Count}");
            }
            else
            {
                Console.WriteLine("No matches");
            }
        }
    }
}
