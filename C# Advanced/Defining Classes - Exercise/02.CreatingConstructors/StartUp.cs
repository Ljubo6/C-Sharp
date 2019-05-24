using System;

namespace DefiningClasses
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Person person = new Person(20);
            Console.WriteLine(person.Age);
        }
    }
}
