using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.Services.Logger;

namespace NQuandl.Services.Quandl
{
    public class QuandlClientDebugDecorator : IQuandlClient
    {
        private readonly Func<IQuandlClient> _clientFactory;
        private readonly ILogger _logger;


        public QuandlClientDebugDecorator([NotNull] Func<IQuandlClient> clientFactory, ILogger logger)
        {
            if (clientFactory == null) throw new ArgumentNullException(nameof(clientFactory));
            _clientFactory = clientFactory;
            _logger = logger;
        }


        public Task<ResultStringWithQuandlResponseInfo> GetStringAsync(QuandlClientRequestParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(QuandlClientRequestParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> GetAsync<TResult>(QuandlClientRequestParameters parameters) where TResult : ResultWithQuandlResponseInfo
        {
            throw new NotImplementedException();
        }
    }
}