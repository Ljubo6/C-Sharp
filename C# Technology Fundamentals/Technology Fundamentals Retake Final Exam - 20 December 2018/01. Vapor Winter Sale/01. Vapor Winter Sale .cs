using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Vapor_Winter_Sale
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> gamePrice = new Dictionary<string, List<string>>();
            string[] input = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string price = string.Empty;
            string game = string.Empty;
            string DLC = string.Empty;
            foreach (var element in input)
            {
                if (element.Contains('-'))
                {
                    string[] tokens = element.Split('-').ToArray();
                    game = tokens[0];
                    price = tokens[1];
                    if (!gamePrice.ContainsKey(game))
                    {
                        gamePrice.Add(game, new List<string>());
                    }
                    gamePrice[game].Add(price);
                }
                else if (element.Contains(':'))
                {
                    string[] tokens = element.Split(':').ToArray();
                    game = tokens[0];
                    DLC = tokens[1];
                    if (gamePrice.ContainsKey(game))
                    {
                        gamePrice[game].Add(DLC);
                        double currentPrice = double.Parse(gamePrice[game][0]);
                        currentPrice *= 1.2;
                        gamePrice[game].Remove(gamePrice[game][0]);
                        price = currentPrice.ToString();
                        gamePrice[game].Insert(0,price);
                    }                    
                }
            }
            foreach (var kvp in gamePrice)
            {
                double currentPrice = double.Parse(kvp.Value[0]);
                if (kvp.Value.Count == 2)
                {
                    currentPrice /= 2;
                }
                else
                {
                    currentPrice *= 0.8;
                }
                kvp.Value.Remove(kvp.Value[0]);
                price = currentPrice.ToString();
                kvp.Value.Insert(0, price);
                kvp.Value.Reverse();
            }
            var withDlc = gamePrice.Where(x => x.Value.Count == 2).ToDictionary(x => x.Key,x => x.Value);
            var withoutDlc = gamePrice.Where(x => x.Value.Count == 1).ToDictionary(x => x.Key, x => x.Value);
            foreach (var kvp in withDlc.OrderBy(x => double.Parse(x.Value[1])))
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value[0]} - {double.Parse(kvp.Value[1]):F2}");
            }
            foreach (var kvp in withoutDlc.OrderByDescending(x => double.Parse(x.Value[0])))
            {
                Console.WriteLine($"{kvp.Key} - {double.Parse(kvp.Value[0]):F2}");
            }
        }
    }
}
