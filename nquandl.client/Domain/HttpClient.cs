using System;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain
{
    public class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
        public HttpClient(string baseUrl)
        {
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");
            base.BaseAddress = new Uri(baseUrl);
        }


    }
}