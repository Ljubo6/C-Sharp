namespace _03.GenericSwapMethodString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> messages = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string message = Console.ReadLine();
                messages.Add(message);
            }
            int[] indexes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Box<string> box = new Box<string>(messages);
            box.Swap(indexes[0],indexes[1]);
            Console.WriteLine(box);
        }
    }
}
