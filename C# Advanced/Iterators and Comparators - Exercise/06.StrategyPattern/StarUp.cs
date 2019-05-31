namespace _06.StrategyPattern
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            SortedSet<Person> peopleByName = new SortedSet<Person>(new PersonByName());
            SortedSet<Person> peopleByAge = new SortedSet<Person>(new PersonByAge());

            while (n-- > 0)
            {
                string[] tokens = Console.ReadLine().Split();
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                Person person = new Person(name,age);

                peopleByName.Add(person);
                peopleByAge.Add(person);
            }

            Console.WriteLine(string.Join(Environment.NewLine,peopleByName));
            Console.WriteLine(string.Join(Environment.NewLine,peopleByAge));
        }
    }
}
