using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;

namespace NQuandl.Client.Api
{
    public interface IHttpClient
    {
        Url Url { get; set; }
        bool AutoDispose { get; }
        HttpClient HttpClient { get; }
        HttpMessageHandler HttpMessageHandler { get; }
        ICollection<string> AllowedHttpStatusRanges { get; }

        Task<HttpResponseMessage> SendAsync(HttpMethod verb, HttpContent content, CancellationToken? cancellationToken,
            HttpCompletionOption completionOption);

        void Dispose();
    }
}