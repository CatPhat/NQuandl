namespace NQuandl.Client.Domain.RequestParameters
{
    public class PathSegmentParameters
    {
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }
        public string ResponseFormat { get; set; }
        public string ApiVersion { get; set; }
    }
}