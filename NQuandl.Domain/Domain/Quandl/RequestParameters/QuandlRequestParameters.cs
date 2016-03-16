using NQuandl.Api.Quandl.Helpers;

namespace NQuandl.Domain.Quandl.RequestParameters
{
    public abstract class QuandlRequestParameters
    {
        public string ApiKey { get; set; }

        public string ApiVersion => RequestParameterConstants.ApiVersion;

        public ResponseFormat ResponseFormat { get; set; }
    }
}