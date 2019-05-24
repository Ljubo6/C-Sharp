namespace _04.OpinionPoll
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            Humans members = new Humans();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string name = input[0];
                int age = int.Parse(input[1]);

                Person person = new Person(name,age);
                members.AddMembers(person);
            }
            List<Person> collection = members.GetOldestCollectionOver30();
            foreach (var member in collection)
            {
                Console.WriteLine($"{member.Name} - {member.Age}");
            }
        }
    }
}
