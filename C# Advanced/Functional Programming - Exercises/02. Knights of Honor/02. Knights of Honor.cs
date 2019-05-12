using System;

namespace _02._Knights_of_Honor
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string[]> print = names => Console.WriteLine("Sir " + string.Join(Environment.NewLine + "Sir ", names));
            string[] inputNames = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
            print(inputNames);
        }
    }
}
