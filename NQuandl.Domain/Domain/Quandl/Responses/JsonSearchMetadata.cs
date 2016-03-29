namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonSearchMetadata
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
}