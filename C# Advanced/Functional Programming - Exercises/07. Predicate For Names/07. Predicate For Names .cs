using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Predicate_For_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<string>> print = p => Console.WriteLine(string.Join("\n",p));
            Predicate<string> predicate;
            int length = int.Parse(Console.ReadLine());

            List<string> input = Console.ReadLine().Split().ToList();

            predicate = GetPredicate(length);

            List<string> names = new List<string>();

            names = input.FindAll(predicate);
            print(names);
        }

        private static Predicate<string> GetPredicate(int length)
        {
            return p => p.Length <= length;
        }
    }
}
