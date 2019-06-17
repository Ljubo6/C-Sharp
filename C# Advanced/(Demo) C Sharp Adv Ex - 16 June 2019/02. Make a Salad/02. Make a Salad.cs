using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Make_a_Salad
{
    class Program
    {
        static void Main(string[] args)
        {
            int tomatoCalory = 80;
            int carrotCalory = 136;
            int lettuceCalory = 109;
            int potatoCalory = 215;
            string[] inputOne = Console.ReadLine().Split();
            int[] inputTwo = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Queue<string> vegetables = new Queue<string>(inputOne);
            Stack<int> calories = new Stack<int>(inputTwo);
            Queue<int> result = new Queue<int>();
            while (vegetables.Any() && calories.Any())
            {
                int calory = calories.Peek();
                
                while (calory > 0 && vegetables.Any())
                {
                    string vegetable = vegetables.Dequeue();
                    if (vegetable == "tomato")
                    {
                        calory -= tomatoCalory;
                    }
                    else if (vegetable == "carrot")
                    {
                        calory -= carrotCalory;
                    }
                    else if (vegetable == "lettuce")
                    {
                        calory -= lettuceCalory;
                    }
                    else if (vegetable == "potato")
                    {
                        calory -= potatoCalory;
                    }
                }
                result.Enqueue(calories.Pop());
            }
            Console.WriteLine(string.Join(" ",result));
            if (calories.Any())
            {
                Console.WriteLine(string.Join(" ", calories));
            }
            if (vegetables.Any())
            {
                Console.WriteLine(string.Join(" ", vegetables));
            }
            
        }
    }
}
