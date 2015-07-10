using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;

namespace NQuandl.Client.Api
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string url);
        void Dispose();
    }
}