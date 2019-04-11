using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace _04._Winning_Ticket
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()).ToArray();
            string pattern = @"(\@{6,}|\${6,}|\^{6,}|\#{6,})";
            Regex regex = new Regex(pattern);
            foreach (var input in inputs)
            {
                //string leftTicket = input.Substring(0,10);
                //string rightTicket = input.Substring(10);
                if (input.Length != 20)
                {
                    Console.WriteLine("invalid ticket");
                    continue;
                }
                Match leftMatch = regex.Match(input.Substring(0, 10));
                Match rightMatch = regex.Match(input.Substring(10, 10));
                int count = Math.Min(leftMatch.Length, rightMatch.Length);

                if (!leftMatch.Success || !rightMatch.Success)
                {
                    Console.WriteLine($"ticket \"{input}\" - no match");
                    continue;
                }

                string leftPart = leftMatch.Value.Substring(0, count);
                string rightPart = rightMatch.Value.Substring(0, count);

                if (leftPart.Equals(rightPart))
                {
                    if (leftPart.Length == 10)
                    {
                        Console.WriteLine($"ticket \"{input}\" - {count}{leftPart.Substring(0, 1)} Jackpot!");
                    }
                    else
                    {
                        Console.WriteLine($"ticket \"{input}\" - {count}{leftPart.Substring(0, 1)}");
                    }
                }
                else
                {
                    Console.WriteLine($"ticket \"{input}\" - no match");
                }
            }
        }
    }
}
