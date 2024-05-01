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

namespace TP2.Lib.MC
{
  
    public abstract class MovieMC
    {
        protected IMovieRepository Repository { get; private set; }
        protected MovieMC(IMovieRepository repository)
        {
            Repository = repository;
        }
    }
}