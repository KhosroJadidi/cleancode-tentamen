using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Models;
using System.Collections.Generic;

namespace MovieLibraryTests
{
    [TestClass]
    public class MovieControllerTests
    {
        [TestMethod]
        public void OrderMoviesAscendingTest()
        {
            var sut = new MovieController();
            var sortedDes = sut.OrderMovies(true, moviesToSort);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(moviesSortedDescendingly[i].Rated, sortedDes[i].Rated);
            }
        }

        [TestMethod]
        public void OrderMoviesDescendingTest()
        {
            var sut = new MovieController();
            var sortedAsc = sut.OrderMovies(false, moviesToSort);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(moviesSortedAscendingly[i].Rated, sortedAsc[i].Rated);
            }
        }

        #region Test Data
        List<MovieWithNumericRating> moviesToSort =
            new List<MovieWithNumericRating>
            {
                new MovieWithNumericRating
                {
                    Id="2",
                    Rated=20,
                    Title="movie 2"
                },
                new MovieWithNumericRating
                {
                    Id="1",
                    Rated=10,
                    Title="movie 1"
                },                
                new MovieWithNumericRating
                {
                    Id="3",
                    Rated=30,
                    Title="movie 3"
                }
            };

        List<MovieWithNumericRating> moviesSortedAscendingly =
            new List<MovieWithNumericRating>
            {
                new MovieWithNumericRating
                {
                    Id="1",
                    Rated=10,
                    Title="movie 1"
                },
                new MovieWithNumericRating
                {
                    Id="2",
                    Rated=20,
                    Title="movie 2"
                },
                new MovieWithNumericRating
                {
                    Id="3",
                    Rated=30,
                    Title="movie 3"
                }
            };

        List<MovieWithNumericRating> moviesSortedDescendingly =
            new List<MovieWithNumericRating>
            {
                new MovieWithNumericRating
                {
                    Id="3",
                    Rated=30,
                    Title="movie 3"
                },
                new MovieWithNumericRating
                {
                    Id="2",
                    Rated=20,
                    Title="movie 2"
                },
                new MovieWithNumericRating
                {
                    Id="1",
                    Rated=10,
                    Title="movie 1"
                }
            };
        #endregion
    }
}
