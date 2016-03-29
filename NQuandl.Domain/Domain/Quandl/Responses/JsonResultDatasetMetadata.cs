using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatasetMetadata : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset")]
        public JsonDatasetMetadata JsonDatasetMetadataMetadata { get; set; }
    }
}