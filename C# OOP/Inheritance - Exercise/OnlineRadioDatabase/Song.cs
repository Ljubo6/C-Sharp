namespace OnlineRadioDatabase
{
    using OnlineRadioDatabase.Exeptions;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Song
    {
        private string artistName;
        private string songName;
        private int minutes;
        private int seconds;

        public Song(string artistName,string songName,int minutes,int seconds)
        {
            this.ArtistName = artistName;
            this.SongName = songName;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }
        public string ArtistName
        {
            get { return artistName; }
            set
            {
                if (value.Length < 3 || value.Length > 20)
                {
                    throw new InvalidArtistNameException();
                }
                artistName = value;
            }
        }
        public string SongName
        {
            get { return songName; }
            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new InvalidSongNameException();
                }
                songName = value;
            }
        }
        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value < 0 || value > 14)
                {
                    throw new InvalidSongMinutesException();
                }
                minutes = value;
            }
        }
        public int Seconds
        {
            get { return seconds; }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new InvalidSongSecondsException();
                }
                seconds = value;
            }
        }

    }
}
