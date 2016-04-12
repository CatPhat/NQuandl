using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResultDatabaseMetadata : ResultWithQuandlResponseInfo
    {
        [JsonProperty("database")]
        public JsonDatabaseMetadata DatabaseMetadata { get; set; }
    }
}