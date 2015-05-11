using System.Collections.Generic;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;
using NQuandl.Client.URIs;

namespace NQuandl.TestConsole
{
    public class TestRequest1Uri : IContainUri
    {
        public string PathSegment
        {
            get { return "testapi"; }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get { return new RequestParameterOptions().ToQueryParameters(); }
        }
    }
}