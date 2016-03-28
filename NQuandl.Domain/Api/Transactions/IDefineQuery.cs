using NQuandl.Api.Quandl.Helpers;

namespace NQuandl.Api.Transactions
{
    public interface IDefineQuandlRequest<TResult>
    {
        string Uri { get; }
    }

    public abstract class BaseQuandlRequest<TResult> : IDefineQuandlRequest<TResult>
    {
        public string ApiVersion => RequestParameterConstants.ApiVersion;
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;
        public string ApiKey { get; set; }

        public string Uri => ToUri();


        public abstract string ToUri();
    }
}