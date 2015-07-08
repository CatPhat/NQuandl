using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;

namespace NQuandl.Client
{
    public class QuandlService : IQuandlService
    {
        private readonly string _baseUrl;

        public QuandlService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<string> GetStringAsync(QuandlRequest<> request)
        {
            var url = GetUrl(request);
            return await GetStringAsync(url);
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await url.GetStringAsync();
        }

        public string GetUrl(QuandlRequest request)
        {
            var baseUrl = new Url(_baseUrl);

            var url = baseUrl.AppendPathSegment(request.PathSegment);
            if (request.QueryParmeters.Any())
            {
                url = url.SetQueryParams(request.QueryParmeters);
            }

            return url;
        }

     

     
    }
}