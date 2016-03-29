using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonResultDatasetSearch : ResultWithQuandlResponseInfo
    {
        [JsonProperty("datasets")]
        public JsonDatasetSearchDataset[] JsonSearchDatasets { get; set; }

        [JsonProperty("meta")]
        public JsonDatasetSearchMetadata JsonSearchMetadata { get; set; }
    }
}