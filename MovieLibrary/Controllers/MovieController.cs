using Microsoft.AspNetCore.Mvc;
using MovieLibrary.DataStorage;
using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace MovieLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        private readonly HttpClient client;

        public MovieController()
        {
            client = new HttpClient();
        }

        [HttpGet]
        [Route("/toplist")]
        public IEnumerable<string> Toplist(bool orderByDescending = true)
        {
            var movies = GetMovies(URLs.Top100);
            //Changed the default ordering to DESCENDING, since the endpoint is called "TOP LIST", and a descending order is more logical for this name.
            movies = OrderMovies(orderByDescending, movies);
            var listOfMovieTitles = GetListOfMovieTitles(movies);
            if (listOfMovieTitles.Count == 0)
                return new List<string> { "Got an empty list, something is wrong" };
            return listOfMovieTitles;
        }

        [HttpGet]
        [Route("/movie")]
        public MovieWithNumericRating GetMovieById(string id)
        {
            var movies = GetMovies(URLs.Top100);

            try
            {
                return movies.SingleOrDefault(movie => movie.Id == id);
            }
            catch (Exception)
            {
                return new MovieWithNumericRating
                {
                    Id = "No such Movie",
                    Rated = 99999999999,
                    Title = "No such Movie"
                };
            }
        }

        #region Helper Methods

        private List<Movie> GetMoviesUnmodified(string url)
        {
            var responseMessage = client.GetAsync(url).Result;
            var stream = responseMessage.Content.ReadAsStream();
            var streamReader = new StreamReader(stream);
            var listOfMovies = JsonSerializer.Deserialize<List<Movie>>
                (streamReader.ReadToEnd());

            return listOfMovies;
        }

        private List<MovieWithNumericRating> ConvertMovieRatingsToFloat(List<Movie> movies)
        {
            var convertedMovies = new List<MovieWithNumericRating>();
            foreach (var movie in movies)
            {
                var ratingAsFloat = float.Parse(movie.Rated);
                var movieWithNumericRating =
                    new MovieWithNumericRating
                    {
                        Id = movie.Id,
                        Rated = ratingAsFloat,
                        Title = movie.Title
                    };
                convertedMovies.Add(movieWithNumericRating);
            }
            return convertedMovies;
        }

        //Visitor pattern!
        private List<MovieWithNumericRating> GetMovies(string url)
        {
            var rawMovies = GetMoviesUnmodified(url);
            var convertedMovies = ConvertMovieRatingsToFloat(rawMovies);
            return convertedMovies;
        }

        public List<MovieWithNumericRating> OrderMovies(bool orderByDescending, List<MovieWithNumericRating> movies)
        {
            if (!orderByDescending)
            {
                movies = movies.OrderBy(e => e.Rated)
                    .ToList();
            }
            else
            {
                movies = movies.OrderByDescending(e => e.Rated)
                    .ToList();
            }

            return movies;
        }

        private List<string> GetListOfMovieTitles(List<MovieWithNumericRating> movies)
        {
            var listOfMovieTitles = new List<string>();
            foreach (var movie in movies)
            {
                listOfMovieTitles.Add(movie.Title);
            }
            return listOfMovieTitles;
        }

        private List<DetailedMovie> GetDetailedMovies(string url)
        {
            
        }

        #endregion Helper Methods
    }
}