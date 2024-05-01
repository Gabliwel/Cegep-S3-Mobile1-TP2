using System;
using System.Collections.Generic;
using System.Text;
using Android.OS;
using Android.Runtime;
using Newtonsoft.Json;

namespace TP2.Lib.Data
{
    public class MovieData
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("movieName")]
        public string MovieName { get; set; }
        [JsonProperty("moviePictureUrl")]
        public string MoviePictureUrl { get; set; }
        [JsonProperty("numLikes")]
        public int NumLikes { get; set; }
        [JsonProperty("numDislikes")]
        public int NumDislikes { get; set; } = 0;

        [JsonConstructor]
        public MovieData(long id = 0, string movieName = "", string moviePictureUrl = "", int numLikes = 0, int numDislikes = 0)
        {
            Id = id;
            MovieName = movieName;
            MoviePictureUrl = moviePictureUrl;
            NumLikes = numLikes;
            NumDislikes = numDislikes;
        }

    }
}
