using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace _03._Emoji_Sumator
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"(?<=[ ,.!?]):[a-z]{4,}:(?=[ ,.!?])";
            Regex regex = new Regex(pattern);
            List<string> emojiFound = new List<string>();

            string input = Console.ReadLine();
            string[] asciiNumbersOfChar = Console.ReadLine().Split(':');

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < asciiNumbersOfChar.Length; i++)
            {
                char symbol = (char)(int.Parse(asciiNumbersOfChar[i]));
                sb.Append(symbol);
            }
            string resultWord = sb.ToString();

            MatchCollection matches = regex.Matches(input);
            bool isCorrespond = false;
            int totalSum = 0;
            foreach (Match match in matches)
            {
                int sum = 0;
                string matchWord = match.Value;
                int start = matchWord[1];
                int length = matchWord.Length - 2;
                string word = matchWord.Substring(1,length);
                if (word == resultWord)
                {
                    isCorrespond = true;
                }
                emojiFound.Add(matchWord);
                for (int i = 0; i < word.Length; i++)
                {
                    sum += word[i];
                }

                totalSum += sum;


            }
            if (isCorrespond)
            {
                totalSum *= 2;
            }
            if (emojiFound.Count > 0)
            {
                Console.WriteLine($"Emojis found: {string.Join(", ",emojiFound)}");
            }
            Console.WriteLine($"Total Emoji Power: {totalSum}");
        }
    }
}
