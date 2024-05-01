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
using TP2.Lib.Database;
using TP2.Lib.Data;

namespace TP2.Lib.MC
{
    public class VoteActivityMC : MovieMC
    {
        public const int VOTE_VALUE = 1;
        public MovieData CurrentMovie { get; set; }
        public bool HasBeenLikedOrDisliked { get; internal set; }   

        public VoteActivityMC(IMovieRepository repository)
            : base(repository)
        {
            HasBeenLikedOrDisliked = false;
        }

        public bool LikeVote()
        {
            MovieData newMovie = new MovieData(CurrentMovie.Id, CurrentMovie.MovieName, CurrentMovie.MoviePictureUrl, CurrentMovie.NumLikes + VOTE_VALUE, CurrentMovie.NumDislikes);
            return Save(newMovie);
        }

        public bool DislikeVote()
        {
            MovieData newMovie = new MovieData(CurrentMovie.Id, CurrentMovie.MovieName, CurrentMovie.MoviePictureUrl, CurrentMovie.NumLikes, CurrentMovie.NumDislikes + VOTE_VALUE);
            return Save(newMovie);
        }

        public void GetNextRandMovie()
        {
            CurrentMovie = Repository.FindRandomMovie();
            HasBeenLikedOrDisliked = false;
        }

        private bool Save(MovieData movie)
        {
            bool success = Repository.Save(movie);
            if (success)
            {
                CurrentMovie = movie;
                HasBeenLikedOrDisliked = true;
            }
            return success;
        }

        public float PctLikes
        {
            get
            {
                int nbLikes = CurrentMovie.NumLikes;
                float totalVote = nbLikes + CurrentMovie.NumDislikes;
                return nbLikes * 100f / totalVote;
            }
        }
        public float PctDislikes
        {
            get
            {
                int nbDislikes = CurrentMovie.NumDislikes;
                float totalVote = nbDislikes + CurrentMovie.NumLikes;
                return nbDislikes * 100f / totalVote;
            }
        }
    }
}