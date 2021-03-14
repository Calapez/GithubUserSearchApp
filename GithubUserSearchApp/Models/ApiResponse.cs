using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokeAppXamarin.Models
{
    class ApiResponse
    {
        [JsonProperty("total_count")] 
        public int total_count { get; set; }

        [JsonProperty("incomplete_results")] 
        public bool incomplete_results { get; set; }

        [JsonProperty("items")] public List<User> 
        items { get; set; }
    }
}