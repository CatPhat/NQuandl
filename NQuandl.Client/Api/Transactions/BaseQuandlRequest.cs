using NQuandl.Client.Api.Quandl.Helpers;

namespace NQuandl.Client.Api.Transactions
{
    public abstract class BaseQuandlRequest<TResult> : IDefineQuandlRequest<TResult>
    {
        public string ApiVersion => RequestParameterConstants.ApiVersion;
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;
        public string ApiKey { get; set; }

        public string Uri => ToUri();


        public abstract string ToUri();
    }
}