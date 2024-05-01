using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TP2.Lib.Data;
using TP2.Lib.Database;

namespace TP2.Lib.MC
{
    class AddMovieMC : MovieMC
    {
        public const long NEW_MOVIE_ID = 0;
        public const int NEW_MOVIE_LIKES_AND_DISLIKES = 0;
        public MovieData Movie { get; internal set; } = new MovieData();
        public AddMovieMC(IMovieRepository repository) : base(repository)
        {
        }
        public bool IsUrlValid { get; set; } = false;

        // TODO: A COMPLETER
        public bool AddMovie(string movieName, string movieImageUrl)
        {
            bool success = false; 
            Movie = new MovieData(NEW_MOVIE_ID, movieName, movieImageUrl, NEW_MOVIE_LIKES_AND_DISLIKES, NEW_MOVIE_LIKES_AND_DISLIKES);
            if (IsUrlValid)
            {
                if(movieName != null && movieName != "")
                {
                    success = Repository.Insert(Movie);
                    if(success)
                    {
                        Movie = new MovieData();
                    }
                }
            }
            return success;
        }
    }
}