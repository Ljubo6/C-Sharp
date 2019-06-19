using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _04._Movie_Time
{
    class Program
    {
        private const string TimeFormat = @"hh\:mm\:ss";
        static void Main(string[] args)
        {
            string favoriteGenre = Console.ReadLine();
            string movieDuration = Console.ReadLine();
            string input = string.Empty;
            Dictionary<string, Dictionary<string, TimeSpan>> movies = new Dictionary<string, Dictionary<string, TimeSpan>>();
            while ((input = Console.ReadLine()) != "POPCORN!")
            {
                string[] tokens = input.Split('|');
                string name = tokens[0];
                string genre = tokens[1];
                TimeSpan duration = TimeSpan.Parse(tokens[2],CultureInfo.InvariantCulture);
                if (!movies.ContainsKey(genre))
                {
                    movies.Add(genre, new Dictionary<string,TimeSpan>()) ;
                }
                if (!movies[genre].ContainsKey(name))
                {
                    movies[genre].Add(name,duration);
                }
            }
            if (movieDuration == "Short")
            {
                movies[favoriteGenre] = movies[favoriteGenre]
                    .OrderBy(x => x.Value)
                    .ThenBy(x => x.Key)
                    .ToDictionary(x => x.Key, y => y.Value);
            }
            else
            {
                movies[favoriteGenre] = movies[favoriteGenre]
                    .OrderByDescending(x => x.Value)
                    .ThenBy(x => x.Key)
                    .ToDictionary(x => x.Key, y => y.Value);
            }
            foreach (var movieKvp in movies[favoriteGenre])
            {
                Console.WriteLine(movieKvp.Key);
                string wifeCommand = Console.ReadLine();
                if (wifeCommand == "Yes")
                {
                    //var totalSeconds = movies.Values.Sum(x => x.Values.Sum(c => c.TotalSeconds));
                      var totalSeconds = movies.Values.Sum(x => x.Sum(s => s.Value.TotalSeconds));

                    int hours = (int)totalSeconds / 60 / 60;
                    int minutes = (int)totalSeconds / 60 % 60;
                    int seconds = (int)totalSeconds % 60;
                    Console.WriteLine($"We're watching {movieKvp.Key} - {movieKvp.Value}");
                    Console.WriteLine($"Total Playlist Duration: {hours:D2}:{minutes:D2}:{seconds:D2}");
                    return;
                }
            }
        }
    }
}
