namespace _1.EvenLines
{
    using System;
    using System.IO;
    using System.Linq;

    class StartUp
    {
        static void Main(string[] args)
        {
            string textFilePath = "text.txt";
            string outputFilePath = "result.txt";
            int counter = 0;
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                using (StreamReader streamReader = new StreamReader(textFilePath))
                {
                    string currentLine = string.Empty;
                    while ((currentLine = streamReader.ReadLine()) != null)
                    {
                        if (counter % 2 == 0)
                        {
                            string replacedSymbols = ReplaceSpecialCharacters(currentLine);
                            string reversedWords = ReverseWords(replacedSymbols);
                            writer.WriteLine(reversedWords);
                        }

                        counter++;
                    }

                }
            }
            
        }
        private static string ReverseWords(string replacedSymbols)
        {
            return string.Join(" ",replacedSymbols.Split().Reverse());
        }

        private static string ReplaceSpecialCharacters(string currentLine)
        {
            return currentLine.Replace("-", "@")
                .Replace(",", "@")
                .Replace(".", "@")
                .Replace("!", "@")
                .Replace("?", "@");
        }
    }
}
