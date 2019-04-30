using System;
using System.Collections.Generic;

namespace _08._Balanced_Parenthesis
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Stack<char> stack = new Stack<char>();
            Stack<int> stackIndex = new Stack<int>();
            int indexLeft = -1;
            int indexRight = -1;
            List<char> chLeft = new List<char>{'{','[','('};
            List<char> chRight = new List<char>{'}',']',')'};

            if (input.Length % 2 != 0 || input.Length == 0)
            {
                Console.WriteLine("NO");
                return;
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (chLeft.Contains(input[i]))
                {
                    stack.Push(input[i]);
                    indexLeft = chLeft.IndexOf(input[i]);
                    stackIndex.Push(indexLeft);
                }
                if (chRight.Contains(input[i]))
                {
                    indexRight = chRight.IndexOf(input[i]);
                    indexLeft = stackIndex.Peek();
                    if (indexLeft == indexRight)
                    {
                        stack.Pop();
                        stackIndex.Pop();                       
                    }
                    else
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                }
            }
            Console.WriteLine("YES");

        }
    }
}
