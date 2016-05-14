using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonDatabaseMetadata
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("datasets_count")]
        public int DatasetsCount { get; set; }

        [JsonProperty("downloads")]
        public int Downloads { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}