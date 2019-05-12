using System;

namespace _01._Action_Print
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string[]> print = p => Console.WriteLine(string.Join("\n",p));
            string[] input = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);

            print(input);
        }
    }
}
