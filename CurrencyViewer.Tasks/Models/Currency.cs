using Newtonsoft.Json;

namespace CurrencyViewer.Tasks.Models
{
    public class Currency
    {
        [JsonProperty(PropertyName = "partitionKey")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Code { get; set; }
    }
}
