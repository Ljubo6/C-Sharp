using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Hornet_Assault
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> beehives = Console.ReadLine().Split(' ').Select(long.Parse).ToList();
            List<long> hornets = Console.ReadLine().Split(' ').Select(long.Parse).ToList();
            for (int i = 0; i < beehives.Count; i++)
            {
                if (hornets.Count == 0)
                {
                    break;
                }
                long hornetSum = hornets.Sum();
                if (beehives[i] < hornetSum)
                {
                    beehives[i] = 0;

                }
                else
                {
                    beehives[i] -= hornetSum;
                    hornets.RemoveAt(0);
                }
            }

            beehives = beehives.Where(x => x > 0).ToList();
            if (beehives.Count > 0)
            {
                Console.WriteLine(string.Join(" ", beehives));
            }
            else
            {
                Console.WriteLine(string.Join(" ", hornets));
            }
        }
    }
}
