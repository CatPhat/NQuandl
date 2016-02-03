namespace NQuandl.Client.Domain.RequestParameters
{
    public class DatabaseListRequestParameters : QuandlRequestParameterWithApiKey
    {
        //optional
        public int? PerPage { get; set; }
        public int? Page { get; set; }
    }
}