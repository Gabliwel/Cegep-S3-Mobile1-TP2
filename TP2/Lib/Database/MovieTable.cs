using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Lib.Database
{
    public class MovieTable
    {
        public const String DROP_TABLE_SQL = "Drop table Movies";
        public const String CREATE_TABLE_SQL = "" +
                "CREATE TABLE Movies (" +
                "       id                     INTEGER          PRIMARY KEY       AUTOINCREMENT, " +
                "       movieName              VARCHAR(100), " +
                "       moviePictureUrl           VARCHAR(1000), "+
                "       numLikes               INTEGER, " +
                "       numDislikes            INTEGER" +
                ")";

        public const String SELECT_SQL_WITH_ID = "Select * from Movies WHERE id=$id";

        public const string SELECT_ALL_OF_SQL = "Select * from Movies";

        public const String INSERT_SQL = "" +
                "INSERT INTO Movies ( " +
                "        movieName, " +
                "        moviePictureUrl, " +
                "        numLikes," +
                "        numDislikes " +
                ") VALUES ( " +
                "        $movieName, " +
                "        $moviePictureUrl, " +
                "        $numLikes," +
                "        $numDislikes " +
                ")";
  
        public const String UPDATE_SQL = "" +
                "UPDATE Movies " +
                "SET " +
                "        movieName = $movieName, " +
                "        moviePictureUrl=$moviePictureUrl, " +
                "        numLikes=$numLikes, " +
                "        numDislikes=$numDislikes " +
                "WHERE " +
                "        id = $id";

        public const String SELECT_LAST_OF_SQL = "" +
                "SELECT " +
                "        MAX(id), " +
                "        Movies.movieName, " +
                "        Movies.moviePictureUrl, " +
                "        Movies.numLikes, " +
                "        Movies.numDislikes " +
                "FROM " +
                        "Movies ";

        public const String DELETE_SQL = "" +
               "DELETE FROM Movies " +
               "WHERE " +
               "        id = $id";

        public const String SELECT_SQL_WITH_RANDOM_ID = "SELECT * FROM Movies ORDER BY RANDOM() LIMIT 1;";
    }
}
