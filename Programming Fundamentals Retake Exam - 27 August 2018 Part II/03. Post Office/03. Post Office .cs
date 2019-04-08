using System;
using System.Text.RegularExpressions;

namespace _03._Post_Office
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split('|');

            string firstPart = input[0];
            string secondPart = input[1];
            string thirdtPart = input[2];

            string patternFirstPart = @"([#$%*&])(?<capitals>[A-Z]+)(\1)";
            Match firstMatch = Regex.Match(firstPart,patternFirstPart);
            string capitals = firstMatch.Groups["capitals"].Value;

            for (int i = 0; i < capitals.Length; i++)
            {
                int asciiCode = capitals[i];
                string patternSecondPart = $@"{asciiCode}:(?<length>[0-9][0-9])";
                Match secondMatch = Regex.Match(secondPart,patternSecondPart);
                int length = int.Parse(secondMatch.Groups["length"].Value);

                string patternThirdPart = $@"(?<=\s|^){capitals[i]}[^\s]{{{length}}}(?=\s|$)";

                Match thirdMatch = Regex.Match(thirdtPart,patternThirdPart);
                string word = thirdMatch.ToString();
                Console.WriteLine(word);
            }

            

        }
    }
}
