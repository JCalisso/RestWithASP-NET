using System.Text.Json.Serialization;

namespace RestWithASPNet.Data.VO
{
    public class PersonVO
    {
        [JsonPropertyName("code")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonIgnore]
        public string Address { get; set; }

        [JsonPropertyName("sex")]
        public string Gender { get; set; }
    }
}
