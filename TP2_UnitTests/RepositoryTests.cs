using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TP2.Lib.Database;
using TP2.Lib;
using TP2.Lib.Data;

namespace TP2_UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        /******************** Test donné ***************************/
        [TestInitialize]
        public void Initialize()
        {
            DataAccessLibrary.Instance.InitializeDatabase(":memory:");
            //Ajout de 3 films dans la base de données pour fins de tests
            string[] THREE_MOVIES =
            {
                "Insert into Movies(id,movieName, moviePictureUrl, numLikes,numDislikes) values (1,\"Terminator\",\"https://upload.wikimedia.org/wikipedia/en/thumb/7/70/Terminator1984movieposter.jpg/220px-Terminator1984movieposter.jpg\", 0, 0)",
                "Insert into Movies(id,movieName, moviePictureUrl, numLikes,numDislikes) values (2,\"Predator\",\"https://upload.wikimedia.org/wikipedia/en/thumb/9/95/Predator_Movie.jpg/220px-Predator_Movie.jpg\", 1,1)",
                "Insert into Movies(id,movieName, moviePictureUrl, numLikes,numDislikes) values (3,\"Raw Deal\",\"https://upload.wikimedia.org/wikipedia/en/thumb/4/45/Raw_deal.jpg/220px-Raw_deal.jpg\", 1,1)",
            };
            DataAccessLibrary.Instance.RunBatchQueries(THREE_MOVIES);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataAccessLibrary.Instance.CloseDatabase();
        }

        [TestMethod]
        public void CanCreateRepository()
        {
            // Arrange
            // Act
            MovieRepository repository = new MovieRepository();

            //// Assert
            Assert.IsNotNull(repository);
        }

        /************************* test de delete ***************************/

        [TestMethod]
        public void When_TryingToDeleteValidId_Then_ReturnsTrue()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            bool result = movieRepository.Delete(1);
            //// Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_TryingToDeleteAnAlreadyDeletedMovie_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            movieRepository.Delete(1);
            bool result = movieRepository.Delete(1);
            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToDeleteInvalidId_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            bool result = movieRepository.Delete(-1);
            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToDeleteInClosedDatabase_Then_ReturnsFalse()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            // Act
            bool result = movieRepository.Delete(1);
            //// Assert
            Assert.IsFalse(result);
        }

        /************************* test de delete 2 ***************************/

        [TestMethod]
        public void When_TryingToDeleteValidId_Then_ReturnsTrue2()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(4, "allo", "bonsoir", 1, 666);
            movieRepository.Insert(movie);
            // Act
            bool result = movieRepository.Delete(movie);
            //// Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_TryingToDeleteAnAlreadyDeletedMovie_Then_ReturnsFalse2()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(4, "allo", "bonsoir", 1, 666);
            movieRepository.Insert(movie);
            // Act
            movieRepository.Delete(movie);
            bool result = movieRepository.Delete(movie);
            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToDeleteInvalidMovie_Then_ReturnsFalse2()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(4, "allo", "bonsoir", 1, 666);
            // Act
            bool result = movieRepository.Delete(movie);
            //// Assert
            Assert.IsFalse(result);
        }

        /************************* test de find last ***************************/

        [TestMethod]
        public void When_TryingToFindLastMovie_Then_ReturnsMovie()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            long expectedID = 3;
            MovieData movie = new MovieData(expectedID, "Raw Deal", "https://upload.wikimedia.org/wikipedia/en/thumb/4/45/Raw_deal.jpg/220px-Raw_deal.jpg", 1, 1);
            // Act
            MovieData result = movieRepository.FindLast();
            //// Assert
            Assert.AreEqual(result.Id, 3);
            AreEqual(movie, result);
        }

        [TestMethod]
        public void When_TryingToFindLastMovieAfterItJustHasBeenDeleted_Then_ReturnsTheMovieBefore()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            long expectedID = 2;
            MovieData movie = new MovieData(expectedID, "Predator", "https://upload.wikimedia.org/wikipedia/en/thumb/9/95/Predator_Movie.jpg/220px-Predator_Movie.jpg", 1, 1);
            // Act
            movieRepository.Delete(3);
            MovieData result = movieRepository.FindLast();
            //// Assert
            AreEqual(movie, result);
        }

        [TestMethod]
        public void When_TryingToFindLastMovieInEmptyDatabase_Then_ReturnsNothing()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            // Act
            movieRepository.Delete(3);
            movieRepository.Delete(2);
            movieRepository.Delete(1);
            MovieData result = movieRepository.FindLast();
            //// Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void When_TryingToFindLastMovieInClosedDatabase_Then_ReturnsNothing()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            // Act
            MovieData result = movieRepository.FindLast();
            //// Assert
            Assert.AreEqual(result, null);
        }

        /************************* test de find ***************************/

        [TestMethod]
        public void When_TryingToFindMovieWithValidId_Then_ReturnsMovie()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            long expectedID = 1;
            MovieData movie = new MovieData(expectedID, "Terminator", "https://upload.wikimedia.org/wikipedia/en/thumb/7/70/Terminator1984movieposter.jpg/220px-Terminator1984movieposter.jpg", 0, 0);
            // Act
            MovieData result = movieRepository.Find(1);
            //// Assert
            AreEqual(movie, result);
        }

        [TestMethod]
        public void When_TryingToFindMovieThatJustHasBeenDeleted_Then_ReturnsNull()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            movieRepository.Delete(1);
            MovieData result = movieRepository.Find(1);
            //// Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void When_TryingToFindMovieWithInalidId_Then_ReturnsNull()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            MovieData result = movieRepository.Find(4);
            //// Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void When_TryingToFindMovieInClosedDatabase_Then_ReturnsNull()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            // Act
            MovieData result = movieRepository.Find(1);
            //// Assert
            Assert.AreEqual(result, null);
        }

        /************************* test de insert ***************************/

        [TestMethod]
        public void When_TryingToInsertValidMovie_Then_ReturnsTrueAndLastMovieIsTheOneInserted()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            long expectedID = 4;
            MovieData movie = new MovieData(expectedID, "allo", "bonsoir", 1, 666);
            // Act
            bool result = movieRepository.Insert(movie);
            MovieData movieResult = movieRepository.FindLast();
            //// Assert
            Assert.IsTrue(result);
            AreEqual(movie, movieResult);
        }

        [TestMethod]
        public void When_TryingToInserMultipletValidMovie_Then_AllOfThemReturnsTrueAndLastMovieIsTheLastOneInserted()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            long expectedID = 4;
            MovieData movie1 = new MovieData(expectedID, "allo1", "bonsoir1", 1, 6);
            MovieData movie2 = new MovieData(expectedID + 1, "allo2", "bonsoir2", 2, 66);
            MovieData movie3 = new MovieData(expectedID + 2, "allo3", "bonsoir3", 3, 666);
            // Act
            bool result1 = movieRepository.Insert(movie1);
            bool result2 = movieRepository.Insert(movie2);
            bool result3 = movieRepository.Insert(movie3);
            MovieData movieResult = movieRepository.FindLast();
            //// Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            AreEqual(movie3, movieResult);
        }

        [TestMethod]
        public void When_TryingToInsertNullMovie_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            bool result = movieRepository.Insert(null);

            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToInsertMovieWithNullValue_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(0, null, null, 0, 0);
            // Act
            bool result = movieRepository.Insert(movie);

            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToInsertMovieInClosedDatabase_Then_ReturnsFalse()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(0, "", "", 0, 0);
            // Act
            bool result = movieRepository.Insert(movie);

            //// Assert
            Assert.IsFalse(result);
        }

        /************************* test de save ***************************/

        [TestMethod]
        public void When_TryingToSaveMovieWithValidInfo_Then_ReturnsTrueAndMovieIsCorrectlySaved()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(1, "allo", "bonsoir", 1, 666);
            // Act
            bool result = movieRepository.Save(movie);
            MovieData movieResult = movieRepository.Find(1);
            //// Assert
            Assert.IsTrue(result);
            AreEqual(movie, movieResult);
        }

        [TestMethod]
        public void When_TryingToSaveMovieAtAnInvalidID_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(4, "allo", "bonsoir", 1, 666);
            // Act
            bool result = movieRepository.Save(movie);
            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToSaveNullMovie_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            bool result = movieRepository.Save(null);
            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToSaveMovieWithNullValue_Then_ReturnsFalse()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(0, null, null, 0, 0);
            // Act
            bool result = movieRepository.Save(movie);
            //// Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_TryingToSaveMovieInClosedDatabase_Then_ReturnsFalse()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            MovieData movie = new MovieData(0, "", "", 0, 0);
            // Act
            bool result = movieRepository.Save(movie);

            //// Assert
            Assert.IsFalse(result);
        }

        /************************* test de find all ***************************/
        [TestMethod]
        public void When_FindingAllMovies_Then_ReturnsAllMoviesInDatabase()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData expectedMovie1 = new MovieData(1, "Terminator", "https://upload.wikimedia.org/wikipedia/en/thumb/7/70/Terminator1984movieposter.jpg/220px-Terminator1984movieposter.jpg", 0, 0);
            MovieData expectedMovie2 = new MovieData(2, "Predator", "https://upload.wikimedia.org/wikipedia/en/thumb/9/95/Predator_Movie.jpg/220px-Predator_Movie.jpg", 1, 1);
            MovieData expectedMovie3 = new MovieData(3, "Raw Deal", "https://upload.wikimedia.org/wikipedia/en/thumb/4/45/Raw_deal.jpg/220px-Raw_deal.jpg", 1, 1);
            // Act
            List<MovieData> movieList = movieRepository.FindAll();

            // Assert
            AreEqual(expectedMovie1, movieList[0]);
            AreEqual(expectedMovie2, movieList[1]);
            AreEqual(expectedMovie3, movieList[2]);
        }

        [TestMethod]
        public void When_FindingAllMoviesAfterDelete_Then_ReturnsAllMoviesInDatabaseExceptTheDeletedOne()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            MovieData expectedMovie1 = new MovieData(1, "Terminator", "https://upload.wikimedia.org/wikipedia/en/thumb/7/70/Terminator1984movieposter.jpg/220px-Terminator1984movieposter.jpg", 0, 0);
            MovieData expectedMovie2 = new MovieData(3, "Raw Deal", "https://upload.wikimedia.org/wikipedia/en/thumb/4/45/Raw_deal.jpg/220px-Raw_deal.jpg", 1, 1);
            // Act
            movieRepository.Delete(2);
            List<MovieData> movieList = movieRepository.FindAll();

            // Assert
            AreEqual(expectedMovie1, movieList[0]);
            AreEqual(expectedMovie2, movieList[1]);
        }

        [TestMethod]
        public void When_FindingAllMoviesAfterDeletingAllOfThem_Then_ReturnsEmptyMovieDataList()
        {
            // Arrange
            MovieRepository movieRepository = new MovieRepository();
            // Act
            movieRepository.Delete(1);
            movieRepository.Delete(2);
            movieRepository.Delete(3);
            List<MovieData> movieList = movieRepository.FindAll();

            // Assert
            Assert.AreEqual(movieList.Count, 0);
        }

        [TestMethod]
        public void When_FindingAllMoviesInClosedDatabase_Then_ReturnsNullMovieDataList()
        {
            // Arrange
            Cleanup();
            MovieRepository movieRepository = new MovieRepository();
            // Act
            List<MovieData> movieList = movieRepository.FindAll();

            // Assert
            Assert.IsNull(movieList);
        }

        /************************* autre ***************************/
        public void AreEqual(MovieData expected, MovieData result)
        {
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.MovieName, result.MovieName);
            Assert.AreEqual(expected.MoviePictureUrl, result.MoviePictureUrl);
            Assert.AreEqual(expected.NumLikes, result.NumLikes);
            Assert.AreEqual(expected.NumDislikes, result.NumDislikes);
        }
    }
}
