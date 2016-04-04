using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDataset : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset_data")]
        public JsonDataset Dataset { get; set; }
    }
}