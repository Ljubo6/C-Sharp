namespace _02.GenericBoxOfInteger
{
    using System;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                int message = int.Parse(Console.ReadLine());
                Box<int> box = new Box<int>(message);
                Console.WriteLine(box);
            }
        }
    }
}
