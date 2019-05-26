namespace _01.GenericBoxOfString
{
    using System;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string message = Console.ReadLine();
                Box<string> box = new Box<string>(message);
                Console.WriteLine(box);
            }
        }
    }
}
