using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class QuandlClientRequestParametersV1
    {
        public PathSegmentParametersV1 PathSegmentParameters { get; set; }
        public ResponseFormat Format { get; set; }
        public RequestParametersV1 RequestParameters { get; set; }
    }
}