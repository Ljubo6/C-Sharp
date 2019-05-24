namespace _05.DateModifier
{
    using System;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string dateOne = Console.ReadLine();
            string dateTwo = Console.ReadLine();
            DateModifier dateModifier = new DateModifier();
            Console.WriteLine(dateModifier.CalculateDiff(dateOne,dateTwo));
        }
    }
}
