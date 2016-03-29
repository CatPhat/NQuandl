using Newtonsoft.Json;

namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonDatabaseSearchMetadata
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        [JsonProperty("current_page")]
        public int? CurrentPage { get; set; }

        [JsonProperty("prev_page")]
        public int? PrevPage { get; set; }

        [JsonProperty("total_pages")]
        public int? TotalPages { get; set; }

        [JsonProperty("total_count")]
        public int? TotalCount { get; set; }

        [JsonProperty("next_page")]
        public int? NextPage { get; set; }

        [JsonProperty("current_first_item")]
        public int? CurrentFirstItem { get; set; }

        [JsonProperty("current_last_item")]
        public int? CurrentLastItem { get; set; }
    }
}