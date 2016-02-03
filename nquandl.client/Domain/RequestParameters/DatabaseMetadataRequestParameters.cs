namespace NQuandl.Client.Domain.RequestParameters
{
    public class DatabaseMetadataRequestParameters : QuandlRequestParameterWithApiKey
    {
        // required
        public string DatabaseCode { get; set; }
    }
}