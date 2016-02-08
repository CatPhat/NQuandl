using System.Net.Http;

namespace NQuandl.Client.Domain.Responses
{
    public abstract class JsonResultWithHttpMessage
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}