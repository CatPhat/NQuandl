namespace NQuandl.Client.Domain.RequestParameters
{
    public class DatasetMetadataRequestParameters : QuandlRequestParameterWithApiKey
    {
        // required
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }
    }
}