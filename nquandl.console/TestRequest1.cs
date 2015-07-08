//using NQuandl.Client.Helpers;
//using NQuandl.Client.Interfaces;
//using NQuandl.Client.Requests;
//using NQuandl.Client.URIs;

//namespace NQuandl.TestConsole
//{
//    public class TestRequest1 : IQuandlRequest
//    {
//        private readonly QueryParametersV2 _queryParameters;
//        public TestRequest1(QueryParametersV2 queryParameters)
//        {
//            _queryParameters = queryParameters;
//        }

//        public IQuandlUri Uri
//        {
//            get { return new QuandlUriV2(ResponseFormat.JSON, _queryParameters); }
//        }
//    }
//}