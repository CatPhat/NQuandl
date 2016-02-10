using System.Net.Http;

namespace NQuandl.Client.Domain.Responses
{
    public abstract class ResponseWithHttpMessage
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}