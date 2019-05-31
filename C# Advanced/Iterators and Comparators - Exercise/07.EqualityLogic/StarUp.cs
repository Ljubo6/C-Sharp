namespace _06.StrategyPattern
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            SortedSet<Person> sortedPeople = new SortedSet<Person>();
            HashSet<Person> peopleByHash = new HashSet<Person>();

            while (n-- > 0)
            {
                string[] tokens = Console.ReadLine().Split();
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                Person person = new Person(name,age);

                sortedPeople.Add(person);
                peopleByHash.Add(person);
            }

            Console.WriteLine(sortedPeople.Count);
            Console.WriteLine(peopleByHash.Count);
        }
    }
}
