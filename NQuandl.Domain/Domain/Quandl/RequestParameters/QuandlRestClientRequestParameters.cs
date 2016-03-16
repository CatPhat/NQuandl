using System.Collections.Generic;

namespace NQuandl.Domain.Quandl.RequestParameters
{
    public class QuandlClientRequestParameters
    {
        public string PathSegment { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
    }
}