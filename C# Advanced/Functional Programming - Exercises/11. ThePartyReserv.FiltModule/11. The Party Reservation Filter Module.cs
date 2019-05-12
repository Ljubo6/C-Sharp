using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._ThePartyReserv.FiltModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<string>> print = x => Console.WriteLine(string.Join(" ",x));
            Predicate<string> predicate;
            List<Dictionary<string, string>> filters = new List<Dictionary<string, string>>();
            List<string> names = Console.ReadLine().Split().ToList();
            string line = string.Empty;
            while ((line = Console.ReadLine()) != "Print")
            {
                string[] input = line.Split(";");
                string command = input[0];
                string filterName = input[1];
                string symbol = input[2];


                if (command == "Add filter")
                {
                    Dictionary<string, string> currentDict = new Dictionary<string, string>();
                    currentDict.Add(filterName, symbol);
                    filters.Add(currentDict);
                }
                else if (command == "Remove filter")
                {
                    for (int i = 0; i < filters.Count; i++)
                    {
                        foreach (var kvp in filters[i])
                        {
                            if (kvp.Key == filterName && kvp.Value == symbol)
                            {
                                filters.Remove(filters[i]);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < filters.Count; i++)
            {
                foreach (var kvp in filters[i])
                {
                    string predicateName = kvp.Key;
                    string predicateSymbol = kvp.Value;

                    predicate = GetPredicate(predicateName,predicateSymbol);

                    names.RemoveAll(predicate);
                }
            }
            print(names);
        }

        private static Predicate<string> GetPredicate(string predicateName, string predicateSymbol)
        {
            if (predicateName == "Starts with")
            {
                return p => p.StartsWith(predicateSymbol);
            }
            else if (predicateName == "Ends with")
            {
                return p => p.EndsWith(predicateSymbol);
            }
            else if (predicateName == "Contains")
            {
                return p => p.Contains(predicateSymbol);
            }
            else if (predicateName == "Length")
            {
                return p =>  p.Length == int.Parse(predicateSymbol);
            }
            return null;
        }
    }
}
