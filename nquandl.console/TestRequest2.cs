using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.URIs;

namespace NQuandl.TestConsole
{
    public class TestRequest2 : IQuandlRequest
    {
        public readonly QueryParametersV2 QueryParameters;

        public TestRequest2(QueryParametersV2 queryParameters)
        {
            QueryParameters = queryParameters;
        }

        public IQuandlUri Uri
        {
            get { return new QuandlUriV2(ResponseFormat.JSON, QueryParameters); }
        }
    }
}