using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MovieLibrary.Controllers
{
    public class Movie
    {
        public string id { get; set; }
        public string title { get; set; }
        public string rated { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        static HttpClient client = new HttpClient();

        [HttpGet]
        [Route("/toplist")]
        public IEnumerable<string> Toplist(bool asc = true)
        {
            List<string> res = new List<string>();
            var r = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var ml = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(r.Content.ReadAsStream()).ReadToEnd());
            //Sort ml
            if (asc)
            {
                ml.OrderBy(e => e.rated);
            }
            else
            {
                ml.OrderByDescending(e => e.rated);
            }
            foreach (var m in ml) {
                res.Add(m.title);
            }
            //result.Add(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());
            return res;
        }
        
        [HttpGet]
        [Route("/movie")]
        public Movie GetMovieById(string id) {
            var r = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var ml = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(r.Content.ReadAsStream()).ReadToEnd());
            foreach (var m in ml) {
                if (m.id.Equals((id)))
                {
                    return m; //Found it!
                }
            }
            return null;
        }
    }
}