using System;
using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonDatasetDataAndMetadata
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        #region metadata
        [JsonProperty("dataset_code")]
        public string DatasetCode { get; set; }

        [JsonProperty("database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("refreshed_at")]
        public DateTime? RefreshedAt { get; set; }

        [JsonProperty("newest_available_date")]
        public string NewestAvailableDate { get; set; }

        [JsonProperty("oldest_available_date")]
        public string OldestAvailableDate { get; set; }

        [JsonProperty("column_names")]
        public string[] ColumnNames { get; set; }

        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("database_id")]
        public int DatabaseId { get; set; }
        #endregion

        #region data
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("transform")]
        public string Transform { get; set; }

        [JsonProperty("column_index")]
        public int? ColumnIndex { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("data")]
        public object[][] Data { get; set; }

        [JsonProperty("collapse")]
        public string Collapse { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
        #endregion

    }
}