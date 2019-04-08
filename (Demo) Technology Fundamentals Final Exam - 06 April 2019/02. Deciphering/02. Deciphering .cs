using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _02._Deciphering
{
    class Program
    {
        private static object stringBuilder;

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] replaceSymbol = Console.ReadLine().Split();
            string pattern = @"^[d-z\{\}|\#]+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(input))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    char ch = (char)(input[i] -3);
                    sb.Append(ch);

                }
                string symbolOne = replaceSymbol[0];
                string symbolTwo = replaceSymbol[1];
                sb.Replace(symbolOne,symbolTwo);
                Console.WriteLine(sb);

            }
            else
            {
                Console.WriteLine("This is not the book you are looking for.");
            }
            

        }
    }
}
