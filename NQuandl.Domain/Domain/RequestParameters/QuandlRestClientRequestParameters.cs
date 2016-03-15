using System.Collections.Generic;

namespace NQuandl.Domain.RequestParameters
{
    public class QuandlClientRequestParameters
    {
        public string PathSegment { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
    }
}