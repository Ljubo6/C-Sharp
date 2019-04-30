using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _09._Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Stack<string> stack = new Stack<string>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                int numbering = int.Parse(tokens[0]);


                if (numbering == 1)
                {
                    string text = tokens[1];
                    stack.Push(sb.ToString());
                    sb.Append(text);
                }
                else if (numbering == 2)
                {
                    int count = int.Parse(tokens[1]);
                    stack.Push(sb.ToString());
                    sb.Remove(sb.Length - count,count);
                }
                else if (numbering == 3)
                {
                    int index = int.Parse(tokens[1]);
                    string str = sb.ToString();
                    Console.WriteLine(str[index - 1]);
                    
                }
                else if (numbering == 4)
                {
                    sb = sb.Clear();
                    sb.Append(stack.Pop());
                }
            }
        }
    }
}
