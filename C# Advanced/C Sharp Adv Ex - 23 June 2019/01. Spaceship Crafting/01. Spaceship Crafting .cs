using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Spaceship_Crafting
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] advancedMaterials = new string[] { "Glass", "Aluminium", "Lithium", "Carbon fiber" };
            Queue<int> chemicalLiquids = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            Stack<int> physicalItems = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());

            Dictionary<string, int> result = new Dictionary<string, int>();
            for (int i = 0; i < advancedMaterials.Length; i++)
            {
                if (!result.ContainsKey(advancedMaterials[i]))
                {
                    result.Add(advancedMaterials[i], 0);
                }
            }

            while (chemicalLiquids.Any() && physicalItems.Any())
            {
                int chemical = chemicalLiquids.Peek();
                int physical = physicalItems.Peek();

                int sum = chemical + physical;
                switch (sum)
                {
                    case 25:
                        chemicalLiquids.Dequeue();
                        physicalItems.Pop();
                        result["Glass"]++;
                        break;
                    case 50:
                        chemicalLiquids.Dequeue();
                        physicalItems.Pop();
                        result["Aluminium"]++;
                        break;
                    case 75:
                        chemicalLiquids.Dequeue();
                        physicalItems.Pop();
                        result["Lithium"]++;
                        break;
                    case 100:
                        chemicalLiquids.Dequeue();
                        physicalItems.Pop();
                        result["Carbon fiber"]++;
                        break;
                    default:
                        chemicalLiquids.Dequeue();
                        physical += 3;
                        physicalItems.Pop();
                        physicalItems.Push(physical);
                        break;
                }

            }
            bool isBuildSpaceship = true;
            foreach (var kvp in result)
            {
                if (kvp.Value == 0)
                {
                    isBuildSpaceship = false;
                }
            }
            if (isBuildSpaceship)
            {
                Console.WriteLine("Wohoo! You succeeded in building the spaceship!");
            }
            else  
            {
                Console.WriteLine("Ugh, what a pity! You didn't have enough materials to build the spaceship.");
            }
            if (chemicalLiquids.Any())
            {
                Console.WriteLine($"Liquids left: {string.Join(", ", chemicalLiquids)}");               
            }
            else
            {
                Console.WriteLine("Liquids left: none");
            }
            if (physicalItems.Any())
            {               
                Console.WriteLine($"Physical items left: {string.Join(", ", physicalItems)}");
            }
            else
            {
                Console.WriteLine($"Physical items left: none");
            }
            foreach (var item in result.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
