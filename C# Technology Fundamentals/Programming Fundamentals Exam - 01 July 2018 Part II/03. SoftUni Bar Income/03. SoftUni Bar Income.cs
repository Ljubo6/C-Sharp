using System;
using System.Text.RegularExpressions;

namespace _03._SoftUni_Bar_Income
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            double totalPrice = 0;
            string pattern = @"([^|$%.]*)%(?<custumer>[A-Z][a-z]+)%([^|$%.]*)<(?<product>\w+)>([^|$%.]*)\|(?<count>\d+)\|([^|$%.]*?)(?<price>\d+.?[\d+]*)\$";
            while ((input = Console.ReadLine()) != "end of shift")
            {
                MatchCollection matched = Regex.Matches(input,pattern);
                foreach (Match matches in matched)
                {
                    double currentPrice = int.Parse(matches.Groups["count"].Value)* double.Parse(matches.Groups["price"].Value);
                    totalPrice += currentPrice;
                    Console.WriteLine($"{matches.Groups["custumer"]}: {matches.Groups["product"]} - {currentPrice:F2}");
                }
            }
            Console.WriteLine($"Total income: {totalPrice:F2}");
        }
    }
}
