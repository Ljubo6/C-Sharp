using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._The_Kitchen
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] inputOne = Console.ReadLine().Split().Select(int.Parse).ToArray(); 
            int[] inputTwo = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Stack<int> knives = new Stack<int>(inputOne);
            Queue<int> forks = new Queue<int>(inputTwo);
            Queue<int> output = new Queue<int>();
            int biggestSet = int.MinValue;
            while (knives.Any() && forks.Any())
            {
                int knife = knives.Peek();
                int fork = forks.Peek();
                if (knife > fork)
                {
                    int sum = knife + fork;
                    output.Enqueue(sum);
                    if (sum > biggestSet)
                    {
                        biggestSet = sum;
                    }
                    knives.Pop();
                    forks.Dequeue();
                }
                else if (knife < fork)
                {
                    knives.Pop();
                }
                else
                {
                    forks.Dequeue();
                    knife = knives.Pop();
                    knife++;
                    knives.Push(knife);

                }
            }
            Console.WriteLine($"The biggest set is: {biggestSet}");
            Console.WriteLine(string.Join(" ",output));
        }
    }
}
