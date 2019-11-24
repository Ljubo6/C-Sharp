using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Anonymous_Cache
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            char[] forbidenSymbols = {' ', '-', '>', '|' };
            Dictionary<string, Dictionary<string, long>> dict = new Dictionary<string, Dictionary<string, long>>();
            Dictionary<string, Dictionary<string, long>> cashe = new Dictionary<string, Dictionary<string, long>>();
            while ((input = Console.ReadLine()) != "thetinggoesskrra")
            {
                bool isValid = true;
                if (input.Contains(" -> "))
                {
                    string[] tokens = input.Split(new[] {" -> "," | "},StringSplitOptions.RemoveEmptyEntries);
                    string dataKey = tokens[0];
                    int dataSize = int.Parse(tokens[1]);
                    string dataSet = tokens[2];
                    for (int i = 0; i < forbidenSymbols.Length; i++)
                    {
                        if (dataKey.Contains(forbidenSymbols[i]) && dataSet.Contains(forbidenSymbols[i]))
                        {
                            isValid = false;
                        }
                    }
                    if (isValid)
                    {
                        if (dict.ContainsKey(dataSet))
                        {
                            dict[dataSet].Add(dataKey,dataSize);
                        }
                        else
                        {
                            if (!cashe.ContainsKey(dataSet))
                            {
                                cashe.Add(dataSet, new Dictionary<string, long>());
                            }
                            cashe[dataSet].Add(dataKey,dataSize);
                        }
                    }
 
                }
                else
                {
                    string dataSet = input;
                    for (int i = 0; i < forbidenSymbols.Length; i++)
                    {
                        if ( dataSet.Contains(forbidenSymbols[i]))
                        {
                            isValid = false;
                        }

                    }
                    if (isValid)
                    {
                        if (cashe.ContainsKey(dataSet))
                        {
                            dict.Add(dataSet, new Dictionary<string, long>(cashe[dataSet]));
                            cashe.Remove(dataSet);
                        }
                        else
                        {
                            dict.Add(dataSet, new Dictionary<string, long>());
                        }

                    }
                }
            }
            if (dict.Count > 0)
            {
                var newDict = dict.OrderByDescending(x => x.Value.Sum(y => y.Value)).First();
                Console.WriteLine($"Data Set: {newDict.Key}, Total Size: {newDict.Value.Sum(x => x.Value)}");
                foreach (var kvp in newDict.Value)
                {
                    Console.WriteLine($"$.{kvp.Key}");
                }
            }
        }
    }
}
