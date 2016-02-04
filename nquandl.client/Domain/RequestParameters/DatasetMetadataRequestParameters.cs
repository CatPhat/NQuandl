namespace NQuandl.Client.Domain.RequestParameters
{
    public class DatasetMetadataRequestParameters : QuandlRequestParameters
    {
        // required
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }
    }
}