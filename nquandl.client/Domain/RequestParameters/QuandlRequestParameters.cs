using NQuandl.Client.Api.Helpers;

namespace NQuandl.Client.Domain.RequestParameters
{
    public abstract class QuandlRequestParameters
    {
        public string ApiKey { get; set; }

        public string ApiVersion => RequestParameterConstants.ApiVersion;

        public ResponseFormat ResponseFormat { get; set; }
    }
}