using System;
using System.Collections.Generic;

namespace _07._SoftUni_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            string guest = string.Empty;
            HashSet<string> vip = new HashSet<string>();
            HashSet<string> regular = new HashSet<string>();
            while ((guest = Console.ReadLine()) != "PARTY")
            {
                if (char.IsDigit(guest[0]))
                {
                    vip.Add(guest);
                }
                else
                {
                    regular.Add(guest);
                }
            }
            guest = string.Empty;
            while ((guest = Console.ReadLine()) != "END")
            {
                if (char.IsDigit(guest[0]))
                {
                    vip.Remove(guest);
                }
                else
                {
                    regular.Remove(guest);
                }
            }
            Console.WriteLine(vip.Count + regular.Count);
            foreach (var v in vip)
            {
                Console.WriteLine(v);
            }
            foreach (var r in regular)
            {
                Console.WriteLine(r);
            }
        }
    }
}
