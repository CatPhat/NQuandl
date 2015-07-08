using System.Collections.Generic;

namespace NQuandl.Client.Domain
{
    public class QuandlRequestParameters
    {
        public string PathSegment { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; }
    }
}