using System;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Helpers;
using NQuandl.Domain.RequestParameters;
using NQuandl.Domain.Responses;

namespace NQuandl.Domain.Queries
{
    public class QuandlQueryBy<TResult> : IDefineQuery<Task<TResult>> where TResult : ResponseWithHttpMessage
    {
        public QuandlQueryBy(QuandlClientRequestParameters requestParameters)
        {
            RequestParameters = requestParameters;
        }

        public QuandlClientRequestParameters RequestParameters { get; }
    }

    public class HandleQuandlQueryBy<TResult> : IHandleQuery<QuandlQueryBy<TResult>, Task<TResult>>
        where TResult : ResponseWithHttpMessage
    {
        private readonly IQuandlClient _client;

        public HandleQuandlQueryBy(IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            _client = client;
        }

        public async Task<TResult> Handle(QuandlQueryBy<TResult> query)
        {
            var httpResponse = await _client.GetFullResponseAsync(query.RequestParameters);
            return await httpResponse.DeserializeToJsonResultAsync<TResult>();
        }
    }
}