using Newtonsoft.Json;

namespace Men.Telegram.ClientApi.Generator.Models
{
    public class TlParam
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}