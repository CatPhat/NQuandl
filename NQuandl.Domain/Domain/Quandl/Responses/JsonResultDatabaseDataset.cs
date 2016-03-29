using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatasetData : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset_data")]
        public JsonDatasetData JsonDatasetData { get; set; }
    }
}