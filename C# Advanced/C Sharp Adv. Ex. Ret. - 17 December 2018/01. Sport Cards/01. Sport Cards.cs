using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Sport_Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, Dictionary<string, double>> cards = new Dictionary<string, Dictionary<string, double>>();
            while ((input = Console.ReadLine()) != "end")
            {
                string[] tokens = input.Split(" - ",StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 3)
                {
                    string cardName = tokens[0];
                    string sportType = tokens[1];
                    double price = double.Parse(tokens[2]);
                    if (!cards.ContainsKey(cardName))
                    {
                        cards.Add(cardName,new Dictionary<string, double>());
                    }
                    if (!cards[cardName].ContainsKey(sportType))
                    {
                        cards[cardName].Add(sportType,price);
                    }
                    else
                    {
                        cards[cardName][sportType] = price;
                    }
                }
                else
                {
                    string[] command = tokens[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string action = command[0];
                    string checkCard = command[1];
                    bool isHaveCard = Checkcard(cards,checkCard);
                    if (isHaveCard)
                    {
                        Console.WriteLine($"{checkCard} is available!");
                    }
                    else
                    {
                        Console.WriteLine($"{checkCard} is not available!");
                    }
                }
            }
            foreach (var kvp in cards.OrderByDescending(x => x.Value.Keys.Count))
            {
                Console.WriteLine($"{kvp.Key}:");
                var sports = kvp.Value;
                foreach (var KVP in sports.OrderBy(x => x.Key))
                {
                    Console.WriteLine($"  -{KVP.Key} - {KVP.Value:F2}");
                }
            }
        }

        private static bool Checkcard(Dictionary<string, Dictionary<string, double>> cards, string checkCard)
        {
            return cards.ContainsKey(checkCard) ? true : false;
        }
    }
}
