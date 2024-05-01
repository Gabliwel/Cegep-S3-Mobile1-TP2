using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Android.Content.Res;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using TP2.Lib.Data;

namespace TP2.Lib.Database
{
  public class DataAccessLibrary
  {
        public SqliteConnection Connection { get; private set; } = null;
        private DataAccessLibrary()
        {

        }
        private static DataAccessLibrary instance = null;
        public static DataAccessLibrary Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new DataAccessLibrary();
                }

                return instance;
            }
        }

        public void InitializeDatabase(string dbpath)
        {
            Connection = new SqliteConnection($"Data Source = {dbpath}");
            Connection.Open();

            try 
            {
                SqliteCommand cmd = Connection.CreateCommand();
                cmd.CommandText = MovieTable.CREATE_TABLE_SQL;
                cmd.ExecuteNonQuery();

                #region Seed              
                //Check if seeded
                SqliteCommand cmdCheckEmpty = Connection.CreateCommand();
                cmdCheckEmpty.CommandText = MovieTable.SELECT_LAST_OF_SQL;
                SqliteDataReader reader = cmdCheckEmpty.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        SeedDatabase();
                    }
                }
                #endregion
            }

            catch (Exception e)
            {
                Debug.WriteLine("Table already exists");
            }
           
        }
        public void CloseDatabase()
        {
            Connection?.Close();
        }

        public void RunBatchQueries(string[] queries)
        {
            foreach (var query in queries)
            {
                SqliteCommand cmd = Connection.CreateCommand();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }          
        }

        private void SeedDatabase()
        {
            string moviesJSON;
            MovieDataCollection moviesCollection;
            MovieRepository repository = new MovieRepository();
            AssetManager assets = Android.App.Application.Context.Assets;
            SqliteCommand cmd = Connection.CreateCommand();
           
            using (StreamReader sr = new StreamReader(assets.Open("movies.json")))
            {
                moviesJSON = sr.ReadToEnd();
            }
            // Ici la chaine moviesJSON contient toutes les informations sur les films à insérer
            // dans la BD

            // TODO : Rendre le MovieData sérialisable
            // TODO : Désérialiser la string Json dans une MovieDataCollection
            //       et utiliser le repository pour insérer tous les films de la collection
            moviesCollection = JsonConvert.DeserializeObject<MovieDataCollection>(moviesJSON);
            foreach(MovieData movie in moviesCollection.Movies)
            {
                repository.Insert(movie);
            }
        }
    }
}
