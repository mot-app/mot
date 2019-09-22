using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mot.Models
{
    [JsonObject]
    public class Meetup
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user1")]
        public string User1 { get; set; }

        [JsonProperty("user2")]
        public string User2 { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
