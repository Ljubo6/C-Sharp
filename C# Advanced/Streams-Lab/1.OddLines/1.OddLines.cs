using System;
using System.IO;

namespace I.File_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "files";
            string fileName = "input.txt";
            string filePath = Path.Combine(path, fileName);

            using (var reader = new StreamReader(filePath))
            {
                int count = 0;
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (count % 2 != 0)
                    {
                        Console.WriteLine(line);
                    }
                    count++;
                    line = reader.ReadLine();
                }
            }
        }
    }
}
