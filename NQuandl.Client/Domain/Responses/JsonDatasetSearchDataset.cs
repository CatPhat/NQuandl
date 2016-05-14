using System;
using Newtonsoft.Json;

namespace NQuandl.Client.Domain.Responses
{
    public class JsonDatasetSearchDataset
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dataset_code")]
        public string DatasetCode { get; set; }

        [JsonProperty("database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("refreshed_at")]
        public DateTime RefreshedAt { get; set; }

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
    }
}