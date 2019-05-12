using System;
using System.Linq;

namespace _03._Count_Uppercase_Words
{
    class Program
    {
        static void Main(string[] args)
        {
             Console.ReadLine()
                .Split(" ",StringSplitOptions.RemoveEmptyEntries)
                .Where(checker)
                .ToList()
                .ForEach(x => Console.WriteLine(x));

        }
        public static Func<string, bool> checker = n => n[0] == n.ToUpper()[0];
    }
}
