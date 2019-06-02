using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3.WordCount
{
    class Program
    {
        static void Main(string[] args)
        {
            string textPath = "text.txt";
            string wordsPath = "words.txt";
            string[] textLines = File.ReadAllLines(textPath);
            string wordsInFile = File.ReadAllText(wordsPath);
            string[] words = wordsInFile.Split();

            Dictionary<string, int> wordsInfo = new Dictionary<string, int>();

            foreach (var word in words)
            {
                string currentWordLowerCase = word.ToLower();
                if (!wordsInfo.ContainsKey(currentWordLowerCase))
                {
                    wordsInfo.Add(currentWordLowerCase, 0);
                }
            }
            foreach (var currentLine in textLines)
            {
                string[] currentLineWords = currentLine
                    .ToLower()
                    .Split(new char[] { ' ', '-', ',', '?', '!', '.', '\'', ':', ';' });
                foreach (var currentWord in currentLineWords)
                {
                    if (wordsInfo.ContainsKey(currentWord))
                    {
                        wordsInfo[currentWord]++;
                    }
                }
            }
            string outputResultPath = "output.txt";
            foreach (var (key, value) in wordsInfo.OrderByDescending(x => x.Value))
            {
                File.AppendAllText(outputResultPath, $"{key} - {value}{Environment.NewLine}");
            }
        }
    }
}
