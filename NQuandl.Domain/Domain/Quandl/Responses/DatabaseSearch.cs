namespace NQuandl.Domain.Quandl.Responses
{
    public class DatabaseSearch : ResultWithQuandlResponseInfo
    {
        public SearchDatabase[] databases { get; set; }
        public SearchMetadata meta { get; set; }
    }

    public class SearchMetadata
    {
        public string query { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public object prev_page { get; set; }
        public int total_pages { get; set; }
        public int total_count { get; set; }
        public int next_page { get; set; }
        public int current_first_item { get; set; }
        public int current_last_item { get; set; }
    }

    public class SearchDatabase
    {
        public int id { get; set; }
        public string name { get; set; }
        public string database_code { get; set; }
        public string description { get; set; }
        public int datasets_count { get; set; }
        public long downloads { get; set; }
        public bool premium { get; set; }
        public string image { get; set; }
    }
}