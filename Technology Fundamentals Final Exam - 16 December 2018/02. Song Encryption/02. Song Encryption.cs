using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _02._Song_Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "end")
            {
                string[] tokens = input.Split(':');
                string artist = tokens[0];
                string song = tokens[1];

                string patternArtist = @"^[A-Z][', a-z]+$";
                string patternSong = @"^[A-Z, ]+$";
                Regex regexArtist = new Regex(patternArtist);
                Regex regexSong = new Regex(patternSong);
                if (regexArtist.IsMatch(artist) && regexSong.IsMatch(song))
                {
                    
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < input.Length; i++)
                    {
                        int key = artist.Length;
                        if (input[i] != ' ' && input[i] != '\'' && input[i] != ':')
                        {

                            char symbol = (char)(input[i] + key);
                            if (char.IsLower(input[i]) && symbol > 'z')
                            {
                                key -= 'z' - input[i] + 1;
                                symbol = (char)('a' + key);
                                sb.Append(symbol);
                            }
                            else if (char.IsUpper(input[i]) && symbol > 'Z')
                            {
                                key -= 'Z' - input[i] + 1;
                                symbol = (char)('A' + key);
                                sb.Append(symbol);
                            }
                           
                            else
                            {
                                sb.Append(symbol);
                            }
                        }
                        else
                        {
                            if (input[i] == ':')
                            {
                                input = input.Replace(input[i], '@');
                            }
                            
                            sb.Append(input[i]);
                        }

                    }

                    Console.WriteLine($"Successful encryption: {sb}");
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }

            }
        }
    }
}
