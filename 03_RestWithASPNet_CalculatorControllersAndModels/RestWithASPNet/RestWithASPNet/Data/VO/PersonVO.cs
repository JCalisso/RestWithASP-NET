﻿//using System.Text.Json.Serialization;

using RestWithASPNet.Hypermedia;
using RestWithASPNet.Hypermedia.Abstract;

namespace RestWithASPNet.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        //[JsonPropertyName("code")]
        public int Id { get; set; }

        //[JsonPropertyName("name")]
        public string FirstName { get; set; }

        //[JsonPropertyName("last_name")]
        public string LastName { get; set; }

        //[JsonIgnore]
        public string Address { get; set; }

        //[JsonPropertyName("sex")]
        public string Gender { get; set; }

        public bool Enabled { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
