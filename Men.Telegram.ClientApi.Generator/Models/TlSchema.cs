using System.Collections.Generic;
using Newtonsoft.Json;

namespace Men.Telegram.ClientApi.Generator.Models
{
    public class TlSchema
    {
        [JsonProperty("constructors")]
        public List<TlConstructor> Constructors { get; set; }

        [JsonProperty("methods")]
        public List<TlMethod> Methods { get; set; }
    }
}