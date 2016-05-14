using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResultDatabaseList : ResultWithQuandlResponseInfo
    {
        [JsonProperty("databases")]
        public JsonDatabaseListDatabase[] Databases { get; set; }
        [JsonProperty("meta")]
        public JsonDatabaseListMetadata Metadata { get; set; }
    }
}