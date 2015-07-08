using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.Client.Domain
{
    public class QuandlRestClientRequestParameters
    {
        public string PathSegment { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
    }

    public class QuandlClientRequestParametersV1
    {
        public PathSegmentParametersV1 PathSegmentParameters { get; set; }
        public ResponseFormat Format { get; set; }
        public QueryParametersV1 QueryParameters { get; set; } 
    }
}