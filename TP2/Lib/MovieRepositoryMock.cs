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
using TP2.Lib.Database;

namespace TP2.Lib
{
    public class MovieRepositoryMock : IMovieRepository
    {
        private List<MovieData> movies = new List<MovieData>();
        public bool Delete(long id)
        {
            return movies.Remove(Find(id));
        }

        public bool Delete(MovieData myObject)
        {
            return movies.Remove(myObject);
        }

        public MovieData Find(long id)
        {
            return movies[(int)id];
        }

        public List<MovieData> FindAll()
        {
            return new List<MovieData>(movies);
        }

        public MovieData FindLast()
        {
            if (movies.Count-1 < 0)
            {
                return movies[movies.Count-1];
            }
            return null;
        }

        public MovieData FindRandomMovie()
        {
            if(movies.Count <= 0)
            {
                return null;
            }
            else
            {
                Random rand = new Random();
                int randIndex = rand.Next(0, movies.Count-1);
                return movies[randIndex];
            }
        }

        public bool Insert(MovieData data)
        {
            if (data != null)
            {
                movies.Add(data);
                return true;
            }
            return false;
        }

        public bool Save(MovieData myObject)
        {
            if (myObject != null && movies[(int)myObject.Id] != null)
            {
                movies[(int)myObject.Id] = myObject;
                return true;
            }
            return false;
        }
    }
}