using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace TP2.Lib.Data
{
    class MovieDataCollection 
    {
        [JsonProperty("movies")]
        public MovieData[] Movies { get; private set; } = null;

        [JsonConstructor]
        public MovieDataCollection(MovieData[] movies)
        {
            this.Movies = movies;
        }
    }
}