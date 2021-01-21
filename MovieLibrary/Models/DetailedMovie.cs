using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieLibrary.Models
{
    public class DetailedMovie
    {
        public class Root
        {            
                [JsonPropertyName("id")]
                public string Id { get; set; }

                [JsonPropertyName("title")]
                public string Title { get; set; }

                [JsonPropertyName("year")]
                public string Year { get; set; }

                [JsonPropertyName("genres")]
                public List<string> Genres { get; set; }

                [JsonPropertyName("ratings")]
                public List<int> Ratings { get; set; }

                [JsonPropertyName("poster")]
                public string Poster { get; set; }

                [JsonPropertyName("contentRating")]
                public string ContentRating { get; set; }

                [JsonPropertyName("duration")]
                public string Duration { get; set; }

                [JsonPropertyName("releaseDate")]
                public string ReleaseDate { get; set; }

                [JsonPropertyName("averageRating")]
                public int AverageRating { get; set; }

                [JsonPropertyName("originalTitle")]
                public string OriginalTitle { get; set; }

                [JsonPropertyName("storyline")]
                public string Storyline { get; set; }

                [JsonPropertyName("actors")]
                public List<string> Actors { get; set; }

                [JsonPropertyName("imdbRating")]
                public double ImdbRating { get; set; }

                [JsonPropertyName("posterurl")]
                public string Posterurl { get; set; }           
        }
    }
}
