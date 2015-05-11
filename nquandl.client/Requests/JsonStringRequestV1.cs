using NQuandl.Client.Interfaces;
using NQuandl.Client.URIs;

namespace NQuandl.Client.Requests
{
    public class JsonStringRequestV1 : IQuandlRequest
    {
        private readonly string _quandlCode;
        public RequestParameterOptions Options { get; set; }
        public JsonStringRequestV1(string quandlCode)
        {
            _quandlCode = quandlCode;
        }

        public IContainUri Uri
        {
            get { return new QuandlJsonUriV1(_quandlCode, Options); }
        }
    }
}
