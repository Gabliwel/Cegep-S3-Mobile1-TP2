using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TP2.Lib.Data;

namespace TP2.Lib.MC
{
    public class MainActivityMC : MovieMC
    {
        public MovieData CurrentMovie { get; internal set; }
        public MainActivityMC(IMovieRepository repository) : base(repository)
        {
            CurrentMovie = null;
        }

        // TODO: A COMPLETER
        public void UseRandomMovie()
        {
            CurrentMovie = Repository.FindRandomMovie();
        }
    }
}