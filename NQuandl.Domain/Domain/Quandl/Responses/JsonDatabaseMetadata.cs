namespace NQuandl.Domain.Quandl.Responses
{
    public class JsonDatabaseMetadata
    {
        public int id { get; set; }
        public string name { get; set; }
        public string database_code { get; set; }
        public string description { get; set; }
        public int datasets_count { get; set; }
        public int downloads { get; set; }
        public bool premium { get; set; }
        public string image { get; set; }
    }
}