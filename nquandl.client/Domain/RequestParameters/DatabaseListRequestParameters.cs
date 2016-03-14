namespace NQuandl.Domain.RequestParameters
{
    public class DatabaseListRequestParameters : QuandlRequestParameters
    {
        //optional
        public int? PerPage { get; set; }
        public int? Page { get; set; }
    }
}