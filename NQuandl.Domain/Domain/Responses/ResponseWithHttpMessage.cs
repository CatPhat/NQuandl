using System.Net.Http;

namespace NQuandl.Domain.Responses
{
    public abstract class ResponseWithHttpMessage
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}