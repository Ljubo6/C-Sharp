using OnlineRadioDatabase.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineRadioDatabase.Core
{
    public class Engine
    {
        private List<Song> songs;
        public Engine()
        {
            this.songs = new List<Song>();
        }
        public void Run()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] inputArgs = Console.ReadLine().Split(';');

                    if (inputArgs.Length != 3)
                    {
                        throw new InvalidSongException();
                    }
                    string artistName = inputArgs[0];
                    string songName = inputArgs[1];
                    string[] lenght = inputArgs[2].Split(':');
                    int minutes = 0;
                    int seconds = 0;
                    bool isMinutes = int.TryParse(lenght[0], out minutes);
                    bool isSeconds = int.TryParse(lenght[1], out seconds);
                    if (!isMinutes)
                    {
                        throw new InvalidSongLengthException();
                    }
                    if (!isSeconds)
                    {
                        throw new InvalidSongLengthException();
                    }

                    Song song = new Song(artistName, songName, minutes, seconds);
                    songs.Add(song);
                    Console.WriteLine("Song added.");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            Print();
        }

        private void Print()
        {
            Console.WriteLine($"Songs added: {songs.Count}");
            int totalSeconds = songs.Sum(x => x.Minutes * 60 + x.Seconds);
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            Console.WriteLine($"Playlist length: {time.Hours}h {time.Minutes}m {time.Seconds}s");
        }
    }
}
