using NQuandl.Client.Api.Helpers;
using NQuandl.Client._OLD.Requests;
using NQuandl.Client._OLD.Requests.old;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class QuandlClientRequestParametersV1
    {
        public PathSegmentParametersV1 PathSegmentParameters { get; set; }
        public ResponseFormat Format { get; set; }
        public QueryParametersV1 QueryParameters { get; set; } 
    }
}