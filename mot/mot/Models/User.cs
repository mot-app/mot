﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace mot.Models
{
    [JsonObject]
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }
    }
}