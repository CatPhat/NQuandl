using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class QuandlClientRequestParametersV2
    {
        public PathSegmentParametersV2 PathSegmentParameters { get; set; }
        public ResponseFormat Format { get; set; }
        public RequestParametersV2 RequestParameters { get; set; }
    }
}