using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Client.Models.Base
{
    public class QuandlContext
    {
        private readonly IQuandlService _quandlService;
        public QuandlContext(IQuandlService quandlService)
        {
            _quandlService = quandlService;
        }

        public async Task<TResponse> GetAsync<TResponse>(OptionalRequestParameters options = null) where TResponse : QuandlResponse
        {
            var response = new object() as TResponse;
            var request = new QuandlRequestV1<TResponse>(response);
            return await _quandlService.GetAsync(request);
        }
      
    }
}
