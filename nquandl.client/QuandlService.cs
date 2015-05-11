using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NQuandl.Client.Interfaces;

namespace NQuandl.Client
{
    public class QuandlService : IQuandlService
    {
        private readonly string _baseUrl;

        public QuandlService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<string> GetStringAsync(IQuandlRequest request)
        {
            var baseUrl = new Url(_baseUrl);


            return
                await
                    baseUrl.AppendPathSegment(request.Uri.PathSegment)
                            .SetQueryParams(request.Uri.QueryParmeters.ToDictionary(x => x.Name, x => x.Value))
                            .GetStringAsync();
        }

       
    }
}