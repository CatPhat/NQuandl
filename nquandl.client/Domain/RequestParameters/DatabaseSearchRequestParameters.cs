namespace NQuandl.Client.Domain.RequestParameters
{
    public class DatabaseSearchRequestParameters : QuandlRequestParameterWithApiKey
    {
        // optional
        public string Query { get; set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
    }
}