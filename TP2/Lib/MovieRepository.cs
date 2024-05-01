using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using TP2.Lib.Database;
using TP2.Lib.Data;

namespace TP2.Lib
{
    public class MovieRepository : IMovieRepository
    {
        public bool Delete(MovieData myObject)
        {
            return Delete(myObject.Id);
        }

        public bool Delete(long id)
        {
            try
            {
                List<SqliteParameter> paramsList = new List<SqliteParameter>
                {
                    new SqliteParameter("$id", id)
                };

                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.DELETE_SQL;
                cmd.Parameters.AddRange(paramsList.ToArray());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MovieData Find(long id)
        {
            try
            {
                List<SqliteParameter> paramsList = new List<SqliteParameter>
                {
                    new SqliteParameter("$id", id)
                };

                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.SELECT_SQL_WITH_ID;
                cmd.Parameters.AddRange(paramsList.ToArray());
                SqliteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.IsDBNull(0) == false)
                    {
                        MovieData movie = new MovieData
                        {
                            Id = (long)reader.GetInt64(0),
                            MovieName = reader.GetString(1),
                            MoviePictureUrl = reader.GetString(2),
                            NumLikes = reader.GetInt32(3),
                            NumDislikes = reader.GetInt32(4)
                        };
                        return movie;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<MovieData> FindAll()
        {
            List<MovieData> movies = null;
            try
            {
                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.SELECT_ALL_OF_SQL;
                SqliteDataReader reader = cmd.ExecuteReader();

                movies = new List<MovieData>();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        MovieData movie = new MovieData
                        {
                            Id = (long)reader.GetInt64(0),
                            MovieName = reader.GetString(1),
                            MoviePictureUrl = reader.GetString(2),
                            NumLikes = reader.GetInt32(3),
                            NumDislikes = reader.GetInt32(4)
                        };
                        movies.Add(movie);
                    }
                }

                return movies;
            }
            catch (Exception)
            {
                return movies;
            }
        }

        public MovieData FindRandomMovie()
        {
            try
            {
                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.SELECT_SQL_WITH_RANDOM_ID;
                SqliteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.IsDBNull(0) == false)
                    {
                        MovieData movie = new MovieData
                        {
                            Id = (long)reader.GetInt64(0),
                            MovieName = reader.GetString(1),
                            MoviePictureUrl = reader.GetString(2),
                            NumLikes = reader.GetInt32(3),
                            NumDislikes = reader.GetInt32(4)
                        };
                        return movie;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(MovieData data)
        {
            try
            {
                List<SqliteParameter> paramsList = new List<SqliteParameter>
                {
                    new SqliteParameter("$movieName", data.MovieName),
                    new SqliteParameter("$moviePictureUrl", data.MoviePictureUrl),
                    new SqliteParameter("$numLikes", data.NumLikes),
                    new SqliteParameter("$numDislikes", data.NumDislikes)
                };

                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.INSERT_SQL;
                cmd.Parameters.AddRange(paramsList.ToArray());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Save(MovieData myObject)
        {
            try
            {
                List<SqliteParameter> paramsList = new List<SqliteParameter>
                {
                    new SqliteParameter("$id", myObject.Id),
                    new SqliteParameter("$movieName", myObject.MovieName),
                    new SqliteParameter("$moviePictureUrl", myObject.MoviePictureUrl),
                    new SqliteParameter("$numLikes", myObject.NumLikes),
                    new SqliteParameter("$numDislikes", myObject.NumDislikes)
                };

                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.UPDATE_SQL;
                cmd.Parameters.AddRange(paramsList.ToArray());
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MovieData FindLast()
        {
            try
            {
                SqliteCommand cmd = DataAccessLibrary.Instance.Connection.CreateCommand();
                cmd.CommandText = MovieTable.SELECT_LAST_OF_SQL;
                SqliteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.IsDBNull(0) == false)
                    {
                        MovieData movie = new MovieData();
                        movie.Id = (long)reader.GetInt64(0);
                        movie.MovieName = reader.GetString(1);
                        movie.MoviePictureUrl = reader.GetString(2);
                        movie.NumLikes = reader.GetInt32(3);
                        movie.NumDislikes = reader.GetInt32(4);
                        return movie;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}