using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Socks
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] leftArr = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[] rightArr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            List<int> result = new List<int>();
            Stack<int> left = new Stack<int>(leftArr);
            Queue<int> right = new Queue<int>(rightArr);

            while (left.Count > 0 && right.Count > 0)
            {
                int currentLeft = left.Peek();
                int currentRight = right.Peek();

                if (currentLeft > currentRight)
                {
                    int sumSocks = currentLeft + currentRight;
                    result.Add(sumSocks);
                    left.Pop();
                    right.Dequeue();
                }
                else if (currentLeft < currentRight)
                {
                    left.Pop();
                }
                else
                {
                    left.Pop();
                    left.Push(currentLeft + 1);
                    right.Dequeue();
                }
            }
            Console.WriteLine(result.OrderByDescending(x => x).FirstOrDefault());
            Console.WriteLine(string.Join(" ",result));
        }
    }
}
