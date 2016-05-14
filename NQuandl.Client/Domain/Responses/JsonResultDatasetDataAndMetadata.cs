using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResultDatasetDataAndMetadata : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset")]
        public JsonDatasetDataAndMetadata DataAndMetadata { get; set; }
    }
}