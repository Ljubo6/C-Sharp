using System;
using System.Text.RegularExpressions;

namespace _03._Regexmon
{
    class Program
    {
        static void Main(string[] args)
        {
            string bojomonPattern = @"[A-Za-z]+-[A-Za-z]+";
            string didimonPattern = @"[^-A-Za-z]+";
            Regex bojoRegex = new Regex(bojomonPattern);
            Regex didiRegex = new Regex(didimonPattern);
            string input = Console.ReadLine();
            while (input != string.Empty)
            {
                if (didiRegex.IsMatch(input))
                {
                    string didiMatch = didiRegex.Match(input).Value;
                    Console.WriteLine(didiMatch);

                    int didiStartIndex = input.IndexOf(didiMatch);
                    input = input.Remove(0,didiStartIndex + didiMatch.Length);
                }
                else
                {
                    break;
                }

                if (bojoRegex.IsMatch(input))
                {
                    string bojoMatch = bojoRegex.Match(input).Value;
                    Console.WriteLine(bojoMatch);

                    int bojoStartIndex = input.IndexOf(bojoMatch);
                    input = input.Remove(0,bojoStartIndex + bojoMatch.Length);
                }
                else
                {
                    break;
                }
            }
        }
    } 
}
