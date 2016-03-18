//using System;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using NQuandl.Api.Quandl;
//using NQuandl.Domain.Quandl.RequestParameters;
//using NQuandl.Domain.Quandl.Responses;

//namespace NQuandl.Services.Quandl
//{
//    public class QuandlClientRateLimiterDecorator : IQuandlClient
//    {
//        private readonly Func<IQuandlClient> _clientFactory;
//        private readonly IRateGate _rateGate;

//        public QuandlClientRateLimiterDecorator([NotNull] Func<IQuandlClient> clientFactory,
//            [NotNull] IRateGate rateGate)
//        {
//            if (clientFactory == null) throw new ArgumentNullException(nameof(clientFactory));
//            if (rateGate == null) throw new ArgumentNullException(nameof(rateGate));
//            _clientFactory = clientFactory;
//            _rateGate = rateGate;
//        }

//        public Task<QuandlClientResponse> GetAsync(QuandlClientRequestParameters parameters)
//        {
//            _rateGate.WaitToProceed();
//            return _clientFactory().GetAsync(parameters);
//        }
//    }
//}