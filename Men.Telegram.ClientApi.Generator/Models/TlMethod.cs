﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Men.Telegram.ClientApi.Generator.Models
{
    public class TlMethod
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<TlParam> Params { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}