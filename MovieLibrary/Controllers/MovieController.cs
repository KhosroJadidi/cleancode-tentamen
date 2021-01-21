using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
            List<string> res = new List<string>();
            var r = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var ml = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(r.Content.ReadAsStream()).ReadToEnd());
            if (asc)
            {
                ml.OrderBy(e => e.Rated);
            }
            else
            {
                ml.OrderByDescending(e => e.Rated);
            }
            foreach (var m in ml) {
                res.Add(m.Title);
            }
            return res;
        }
        
        [HttpGet]
        [Route("/movie")]
        public Movie GetMovieById(string id) {
            var r = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var ml = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(r.Content.ReadAsStream()).ReadToEnd());
            foreach (var m in ml) {
                if (m.Id.Equals((id)))
                {
                    return m; 
                }
            }
            return null;
        }
    }
}