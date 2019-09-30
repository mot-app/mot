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
        public string Id { get; set; }

        [JsonProperty("user1")]
        public string User1 { get; set; }

        [JsonProperty("user2")]
        public string User2 { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("user1name")]
        public string User1Name { get; set; }

        [JsonProperty("user2name")]
        public string User2Name { get; set; }
    }
}
