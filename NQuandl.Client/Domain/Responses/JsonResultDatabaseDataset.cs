using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResultDataset : ResultWithQuandlResponseInfo
    {
        [JsonProperty("dataset_data")]
        public JsonDataset Dataset { get; set; }
    }
}