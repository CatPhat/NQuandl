using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatabaseMetadata : ResultWithQuandlResponseInfo
    {
        [JsonProperty("database")]
        public JsonDatabaseMetadata DatabaseMetadata { get; set; }
    }
}