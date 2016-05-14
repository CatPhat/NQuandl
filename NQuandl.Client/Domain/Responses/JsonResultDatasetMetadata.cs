using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResultDatasetMetadata : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset")]
        public JsonDatasetMetadata JsonDatasetMetadataMetadata { get; set; }
    }
}