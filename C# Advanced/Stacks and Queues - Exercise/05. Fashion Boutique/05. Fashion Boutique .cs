using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Fashion_Boutique
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] clothes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Stack<int> stack = new Stack<int>(clothes);
            int rackVolume = int.Parse(Console.ReadLine());
            int sumClothes = 0;
            int rackNum = 1;
            while (stack.Count > 0)
            {
                int currentCloth = stack.Peek();
                int currentSum = currentCloth + sumClothes;
                if (currentSum < rackVolume)
                {
                    sumClothes += currentCloth;
                    stack.Pop();
                }
                else if (currentSum == rackVolume)
                {
                    sumClothes += currentCloth;
                    stack.Pop();
                    if (stack.Count > 0)
                    {
                        sumClothes = 0;
                        rackNum++;
                    }
                }
                else if (currentSum > rackVolume)
                {
                    sumClothes = 0;
                    rackNum++;
                }
            }
            Console.WriteLine(rackNum);
        }
    }
}
