using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _01._Arriving_in_Kathmandu
{
    class Program
    {
        private static object regex;

        static void Main(string[] args)
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "Last note")
            {
                StringBuilder sb = new StringBuilder();
                int n = 0;
                string patternNumber = @"(?<=\=)([0-9]+)";
                Regex regexNumber = new Regex(patternNumber);
                Match num = regexNumber.Match(input);
                if (num.Success)
                {
                    n = int.Parse(num.Value.ToString());
                }
                string pattern = $@"^(?<name>[A-Za-z0-9!@#$?]+)=(?<number>([0-9]+))<<(?<coordinate>.{{{n}}}$)";
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    string name = match.Groups["name"].Value.ToString();
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (char.IsLetterOrDigit(name[i]))
                        {
                            sb.Append(name[i]);
                        }
                    }
                    name = sb.ToString();
                    string coorditane = match.Groups["coordinate"].Value.ToString();

                    Console.WriteLine($"Coordinates found! {name} -> {coorditane}");
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
                
            }
        }
    }
}
