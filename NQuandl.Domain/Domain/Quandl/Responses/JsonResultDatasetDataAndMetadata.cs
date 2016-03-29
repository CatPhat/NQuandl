using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatasetDataAndMetadata : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset")]
        public JsonDatasetDataAndMetadata DataAndMetadata { get; set; }
    }
}