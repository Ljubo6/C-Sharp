using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Trojan_Invasion
{
    class Program
    {
        static void Main(string[] args)
        {
            int waves = int.Parse(Console.ReadLine());
            List<int> plates = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            Stack<int> trojans = new Stack<int>();
            for (int i = 1; i <= waves; i++)
            {
                if (plates.Count == 0)
                {
                    break;
                }
                int[] trojanWarriors = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
                trojans = new Stack<int>(trojanWarriors);
                if (i % 3 == 0)
                {
                    int plateToAdd = int.Parse(Console.ReadLine());
                    plates.Add(plateToAdd);
                }
                while (trojans.Any() && plates.Any())
                {
                    int trojanWarrior = trojans.Pop();
                    if (trojanWarrior > plates[0])
                    {
                        trojanWarrior -= plates[0];
                        trojans.Push(trojanWarrior);
                        plates.RemoveAt(0);
                    }
                    else if (trojanWarrior < plates[0])
                    {
                        plates[0] -= trojanWarrior;
                    }
                    else
                    {
                        plates.RemoveAt(0);
                    }
                }
            }
            if (trojans.Any())
            {
                Console.WriteLine("The Trojans successfully destroyed the Spartan defense.");
                Console.WriteLine($"Warriors left: {string.Join(", ",trojans)}");
            }
            else if (plates.Any())
            {
                Console.WriteLine("The Spartans successfully repulsed the Trojan attack.");
                Console.WriteLine($"Plates left: {string.Join(", ",plates)}");
            }
        }
    }
}
