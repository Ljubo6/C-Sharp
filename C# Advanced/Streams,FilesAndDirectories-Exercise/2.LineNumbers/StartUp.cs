namespace _2.LineNumbers
{
    using System;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string textPath = "text.txt";
            string outputPath = "output.txt";
            string[] textLines = File.ReadAllLines(textPath);
            int lineCounter = 1;
            foreach (var currentLine in textLines)
            {
                int lettersCount = currentLine.Count(char.IsLetter);
                int puncsCount = currentLine.Count(char.IsPunctuation);
                File.AppendAllText(outputPath,$"Line {lineCounter}: {currentLine} ({lettersCount})({puncsCount}){Environment.NewLine}");
                lineCounter++;
            }
        }
    }
}
