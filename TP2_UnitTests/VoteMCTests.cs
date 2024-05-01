using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP2.Lib.Data;
using TP2.Lib;
using TP2.Lib.MC;

namespace TP2_UnitTests
{
    /// <summary>
    /// Summary description for VoteMCTests
    /// </summary>
    [TestClass]
    public class VoteMCTests
    {
        /*----------------------------Dislike tests--------------------------------*/
        
        [TestMethod]
        public void MC01_canLikeMovieWithNoExistingLikeAndDislike()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 0, 0);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.LikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 100);
            Assert.AreEqual(voteMC.PctDislikes, 0);
        }
        
        [TestMethod]
        public void MC02_canLikeMovieWithExistingLikesAndDislikes()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 1, 2);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.LikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 50);
            Assert.AreEqual(voteMC.PctDislikes, 50);
        }

        [TestMethod]
        public void MC03_canLikeMovieWithExistingLikesButNoDislikes()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 1, 0);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.LikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 100);
            Assert.AreEqual(voteMC.PctDislikes, 0);
        }

        [TestMethod]
        public void MC04_canLikeMovieWithExistingDisLikesButNoLike()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 0, 1);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.LikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 50);
            Assert.AreEqual(voteMC.PctDislikes, 50);
        }
        


        /*----------------------------Dislike tests--------------------------------*/
        
        [TestMethod]
        public void MC05_canDislikeMovieWithNoExistingLikeAndDislike()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 0, 0);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.DislikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 0);
            Assert.AreEqual(voteMC.PctDislikes, 100);
        }

        [TestMethod]
        public void MC06_canDislikeMovieWithExistingLikesAndDislikes()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 2, 1);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.DislikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 50);
            Assert.AreEqual(voteMC.PctDislikes, 50);
        }

        [TestMethod]
        public void MC07_canDislikeMovieWithExistingLikesButNoDislikes()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 1, 0);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.DislikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 50);
            Assert.AreEqual(voteMC.PctDislikes, 50);
        }

        [TestMethod]
        public void MC08_canDislikeMovieWithExistingDisLikesButNoLike()
        {
            //Arrange
            MovieRepositoryMock repo = new MovieRepositoryMock();
            VoteActivityMC voteMC = new VoteActivityMC(repo);
            MovieData movie = new MovieData(0, "", "", 0, 1);
            repo.Insert(movie);
            voteMC.CurrentMovie = movie;
            //Act
            voteMC.DislikeVote();
            //Assert
            Assert.AreEqual(voteMC.PctLikes, 0);
            Assert.AreEqual(voteMC.PctDislikes, 100);
        }
    }
}
