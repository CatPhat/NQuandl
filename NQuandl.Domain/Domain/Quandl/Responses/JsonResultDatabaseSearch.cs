using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatabaseSearch : ResultWithQuandlResponseInfo
    {
        [JsonProperty("databases")]
        public JsonDatabaseSearch[] JsonDatabaseSearchDatabases { get; set; }

        [JsonProperty("meta")]
        public JsonDatabaseSearchMetadata JsonDatabaseSearchMetadata { get; set; }
    }
}