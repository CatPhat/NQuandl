using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonDataset
    {
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("transform")]
        public string Transform { get; set; }

        [JsonProperty("column_index")]
        public int? ColumnIndex { get; set; }

        [JsonProperty("column_names")]
        public string[] ColumnNames { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        [JsonProperty("data")]
        public object[][] Data { get; set; }

        [JsonProperty("collapse")]
        public string Collapse { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
    }
}