using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _04._Cubic_Messages
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "Over!")
            {
                int wordLength = int.Parse(Console.ReadLine());
                string pattern = $@"^(?<frontNumbers>\d+)(?<word>[A-Za-z]{{{wordLength}}})(?<backSymbols>[^A-Za-z]*)$";
                Regex regex = new Regex(pattern);

                Match match = regex.Match(input);
                if (match.Success)
                {
                    string frontNumbers = match.Groups["frontNumbers"].Value.ToString();
                    string word = match.Groups["word"].Value;
                    string backSymbols = match.Groups["backSymbols"].Value.ToString();
                    string verification = frontNumbers + backSymbols;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < verification.Length; i++)
                    {
                        if (char.IsDigit(verification[i]))
                        {
                            int index = int.Parse(verification[i].ToString());
                            if (index >= 0 && index < word.Length)
                            {
                                sb.Append(word[index]);
                            }
                            else
                            {
                                sb.Append(" ");
                            }
                        }

                    }
                    Console.WriteLine($"{word} == {sb}");
                }
            }
        }
    }
}
