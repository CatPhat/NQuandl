using Flurl.Http;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain
{
    public class HttpClient : FlurlClient, IHttpClient
    {
    }
}