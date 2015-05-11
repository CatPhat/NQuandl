using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;
using NQuandl.Client.URIs;

namespace NQuandl.TestConsole
{
    public class TestRequest2Uri : IContainUri
    {
        public string PathSegment
        {
            get { return "testapi2"; }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get { return new RequestParameterOptions().ToQueryParameters(); }
        }
    }
}