using System;
using System.Collections.Generic;
using System.Linq;


namespace _01._Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordsList = new List<string>();
            Dictionary<string, List<string>> dictWords = new Dictionary<string, List<string>>();
            string[] input = Console.ReadLine().Split(" | ");
            string[] words = Console.ReadLine().Split(" | ");

            for (int i = 0; i < input.Length; i++)
            {
                string[] tokens = input[i].Split(": ");
                string word = tokens[0];
                string definition = tokens[1];
                if (!dictWords.ContainsKey(word))
                {
                    dictWords.Add(word, new List<string>());
                }
                dictWords[word].Add(definition);
                wordsList.Add(word);

            }
            foreach (var word in words)
            {
                foreach (var kvp in dictWords)
                {
                    if (kvp.Key == word)
                    {
                        List<string> current = kvp.Value;
                        current = current.OrderByDescending(x => x.Count()).ToList();
                        Console.WriteLine($"{kvp.Key}");
                        Console.Write(" -");
                        Console.WriteLine($"{string.Join(Environment.NewLine + " -", current)}");

                    }
                }
            }
            string command = Console.ReadLine();
            if (command == "End")
            {
                return;
            }
            if (command == "List")
            {

                wordsList = wordsList.Distinct().OrderBy(x => x).ToList();
                Console.WriteLine(string.Join(" ", wordsList));

            }

        }
    }
}
