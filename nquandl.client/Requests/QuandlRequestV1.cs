using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;

namespace NQuandl.Client.Requests
{
    public class QuandlRequestV1 : IReturn<QuandlFullDataResponseV1>
    {
        private readonly RequestParameters _parameters;
        public QuandlRequestV1(RequestParameters parameters)
        {
            _parameters = parameters;
        }

        public string Url
        {
            get { return _parameters.ToV1Url(); }
        }
    }
}
