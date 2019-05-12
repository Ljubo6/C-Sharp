using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Sum_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = Console.ReadLine().Split(", ").Select(Parse).ToList();
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Sum());
        }

        public static Func<string, int> Parse = n => int.Parse(n);
    }
}
