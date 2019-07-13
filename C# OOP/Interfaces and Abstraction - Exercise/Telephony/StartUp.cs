using System;

namespace Telephony
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Smartphone smartfhone = new Smartphone();
            string[] inputNumbers = Console.ReadLine().Split();
            string[] inputUrls = Console.ReadLine().Split();
            for (int i = 0; i < inputNumbers.Length; i++)
            {
                smartfhone.Call(inputNumbers[i]);
            }
            for (int i = 0; i < inputUrls.Length; i++)
            {
                smartfhone.Browse(inputUrls[i]);
            }
        }
    }
}
