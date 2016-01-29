using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class QuandlClientRequestParameters
    {
        public PathSegmentParameters PathSegmentParameters { get; set; }
        public ResponseFormat Format { get; set; }
        public RequestParameters RequestParameters { get; set; }
    }
}