using System.Collections.Generic;
using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public class QuandlClientRequestParameters
    {
        public PathSegmentParameters PathSegmentParameters { get; set; }
        public Dictionary<string,string> RequestParametersDictionary { get; set; }
    }
}