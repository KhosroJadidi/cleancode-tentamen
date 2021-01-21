using System.Text.Json.Serialization;

namespace MovieLibrary.Models

{
    public class Movie
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("rated")]
        public string Rated { get; set; }
    }
}