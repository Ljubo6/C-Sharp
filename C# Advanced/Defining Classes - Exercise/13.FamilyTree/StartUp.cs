namespace _13.FamilyTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        static List<Person> persons;
        static List<string> relationships;
        static void Main(string[] args)
        {
            persons = new List<Person>();
            relationships = new List<string>();

            string mainPersonInfo = Console.ReadLine();

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                if (!input.Contains("-"))
                {
                    AddMember(input);
                    continue;
                }
                relationships.Add(input);
            }

            foreach (var memberInfo in relationships)
            {
                string[] inputArg = memberInfo.Split(" - ", StringSplitOptions.RemoveEmptyEntries);

                Person parent = GetPerson(inputArg[0]);
                Person child = GetPerson(inputArg[1]);

                if (!parent.Children.Contains(child))
                {
                    parent.Children.Add(child);
                }
                if (!child.Parents.Contains(parent))
                {
                    child.Parents.Add(parent);
                }
                
            }
            Print(mainPersonInfo);
        }

        private static void Print(string mainPersonInfo)
        {
            Person mainPerson = GetPerson(mainPersonInfo);
            Console.WriteLine($"{mainPerson.Name} {mainPerson.Birthday}");

            Console.WriteLine("Parents:");

            foreach (var parent in mainPerson.Parents)
            {
                Console.WriteLine($"{parent.Name} {parent.Birthday}");
            }

            Console.WriteLine("Children:");

            foreach (var child in mainPerson.Children)
            {
                Console.WriteLine($"{child.Name} {child.Birthday}");
            }
        }

        private static Person GetPerson(string input)
        {
            if (input.Contains("/"))
            {
                return persons.FirstOrDefault(x => x.Birthday == input);
            }

            return persons.FirstOrDefault(x => x.Name == input);
        }

        private static void AddMember(string input)
        {
            string[] inputArg = input.Split();

            string name = inputArg[0] + " " + inputArg[1];
            string birthday = inputArg[2];

            Person person = new Person(name,birthday);
            persons.Add(person);
        }
    }
}
