using System;
using System.Collections.Generic;

namespace _03._Product_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            SortedDictionary<string, List<string>> shopProduct = new SortedDictionary<string, List<string>>();
            SortedDictionary<string, List<double>> shopPrice = new SortedDictionary<string, List<double>>();
            while ((input = Console.ReadLine()) != "Revision")
            {
                string[] tokens = input.Split(", ",StringSplitOptions.RemoveEmptyEntries);
                string store = tokens[0];
                string product = tokens[1];
                double price = double.Parse(tokens[2]);
                if (!shopProduct.ContainsKey(store))
                {
                    shopProduct.Add(store,new List<string>());
                    shopPrice.Add(store,new List<double>());
                }
                shopProduct[store].Add(product);
                shopPrice[store].Add(price);

            }
            foreach (var kvp in shopProduct)
            {
                Console.WriteLine($"{kvp.Key}->");
                var products = kvp.Value;
                var prices = shopPrice[kvp.Key];
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"Product: {products[i]}, Price: {prices[i]}");
                }

            }
        }
    }
}
