using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task3_3_
{
    internal class Song
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Composer { get; set; }
        public int Year { get; set; }
        public string LyricsFileName { get; set; }
        public string[] Performers { get; set; }

        public Song()
        {
            Title = "";
            Author = "";
            Composer = "";
            Year = 0;
            LyricsFileName = "";
            Performers = new string[0];
        }

        public Song(string title, string author, string composer, int year, string lyricsFileName, string[] performers)
        {
            Title = title;
            Author = author;
            Composer = composer;
            Year = year;
            LyricsFileName = lyricsFileName;
            Performers = performers;
        }
    }
}
