using System;
using System.Collections.Generic;

namespace _06._Parking_Lot
{
    class Program
    {
        static void Main(string[] args)
        {
            string entry = string.Empty;

            HashSet<string> hashSet = new HashSet<string>();

            while ((entry = Console.ReadLine()) != "END")
            {
                string[] tokens = entry.Split(", ");
                string command = tokens[0];
                string carNumber = tokens[1];

                if (command == "IN")
                {
                    hashSet.Add(carNumber);
                }
                else
                {
                    hashSet.Remove(carNumber);
                }
            }
            if (hashSet.Count > 0)
            {
                foreach (var number in hashSet)
                {
                    Console.WriteLine(number);
                }
            }
            else
            {
                Console.WriteLine("Parking Lot is Empty");
            }

        }
    }
}
