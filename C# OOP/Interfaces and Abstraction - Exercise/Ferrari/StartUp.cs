using System;

namespace Ferrari
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string driverName = Console.ReadLine();
            Ferrari ferrari = new Ferrari(driverName);
            Console.WriteLine(ferrari);
        }
    }
}
