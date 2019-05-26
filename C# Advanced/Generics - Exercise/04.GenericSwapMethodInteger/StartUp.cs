namespace _04.GenericSwapMethodInteger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> messages = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int message = int.Parse(Console.ReadLine());
                messages.Add(message);
            }
            int[] indexes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Box<int> box = new Box<int>(messages);
            box.Swap(indexes[0],indexes[1]);
            Console.WriteLine(box);
        }
    }
}
