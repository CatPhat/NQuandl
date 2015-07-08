namespace NQuandl.Client.Requests
{
    public class QueryParametersV2
    {
        public string ApiKey { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Query { get; set; }
        public string SourceCode { get; set; }
    }
}