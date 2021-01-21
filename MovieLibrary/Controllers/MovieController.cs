using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.DataStorage;
using MovieLibrary.Models;

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
        public IEnumerable<string> Toplist(bool asc = true)
        {
            var listOfMovieTitles = new List<string>();
            var movies = GetMovies(URLs.Top100);

            if (asc)
            {
                movies.OrderBy(e => e.Rated);
            }
            else
            {
                movies.OrderByDescending(e => e.Rated);
            }
            foreach (var movie in movies) {
                listOfMovieTitles.Add(movie.Title);
            }
            return listOfMovieTitles;
        }
        
        //[HttpGet]
        //[Route("/movie")]
        //public Movie GetMovieById(string id) 
        //{
        //    var movies = GetMovies();
        //    foreach (var movie in movies) {
        //        if (movie.Id.Equals((id)))
        //        {
        //            return movie; 
        //        }
        //    }
        //    return null;
        //}

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

        private List<MovieWithNumericRating> GetMovies(string url)
        {
            var rawMovies = GetMoviesUnmodified(url);
            var convertedMovies = ConvertMovieRatingsToFloat(rawMovies);
            return convertedMovies;
        }
        #endregion
    }
}