namespace NQuandl.Client.Domain.RequestParameters
{
    public class DatabaseMetadataRequestParameters : QuandlRequestParameters
    {
        // required
        public string DatabaseCode { get; set; }
    }
}