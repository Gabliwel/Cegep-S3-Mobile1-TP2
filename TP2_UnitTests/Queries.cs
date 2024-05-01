using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_UnitTests
{
  static class Queries
  {
    public static readonly string[] ONE_MOVIE =
    {
      "Insert into Movies(id,movieName, moviePictureUrl, numLikes,numDislikes) values (1,\"Terminator\",\"http://www.any_url.com\", 0, 0)"
    };
    public static readonly string[] TWO_MOVIES =
    {
      "Insert into Movies(id,movieName, moviePictureUrl, numLikes,numDislikes) values (1,\"Terminator\",\"http://www.any_url.com\", 0, 0)",
      "Insert into Movies(id,movieName, moviePictureUrl, numLikes,numDislikes) values (2,\"Predator\",\"http://www.any_url2.com\", 1,1)"
    };
  }
}
