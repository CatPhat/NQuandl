using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonResultDatasetSearch : ResultWithQuandlResponseInfo
    {
        [JsonProperty("datasets")]
        public JsonDatasetSearchDataset[] JsonSearchDatasets { get; set; }

        [JsonProperty("meta")]
        public JsonDatasetSearchMetadata JsonSearchMetadata { get; set; }
    }
}