using Newtonsoft.Json;

namespace PokeAppXamarin
{
    class User
    {
        [JsonProperty("login")] 
        public string login { get; set; }
    }
}