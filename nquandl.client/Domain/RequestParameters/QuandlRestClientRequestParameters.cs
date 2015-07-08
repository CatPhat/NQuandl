using System.Collections.Generic;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class QuandlRestClientRequestParameters
    {
        public string PathSegment { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
    }
}