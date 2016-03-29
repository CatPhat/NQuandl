namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonDatabaseListMetadata
    {
        public int? current_page { get; set; }
        public int? next_page { get; set; }
        public int? prev_page { get; set; }
        public int? total_pages { get; set; }
        public int? total_count { get; set; }
        public int? per_page { get; set; }
        public int? current_first_item { get; set; }
        public int? current_last_item { get; set; }
    }
}