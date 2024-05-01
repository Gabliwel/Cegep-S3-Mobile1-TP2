using System.Collections.Generic;
using TP2.Lib.Data;

namespace TP2.Lib
{
    public interface IMovieRepository : IRepository<MovieData>
    {
        bool Delete(MovieData myObject);
        List<MovieData> FindAll();
        MovieData FindRandomMovie();
        MovieData FindLast();
    }
}