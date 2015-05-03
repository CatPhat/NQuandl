using NQuandl.Client.Entities;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;

namespace NQuandl.Client.Requests
{
    public class QuandlRequestV1 : IQuandlRequest
    {
        private readonly RequestParameters _parameters;

        public QuandlRequestV1(RequestParameters parameters)
        {
            _parameters = parameters;
        }

        public string Url
        {
            get
            {
                string url = QuandlServiceConfiguration.BaseUrl + "/" + RequestParameterConstants.Version1Format + "/" +
                             _parameters.QuandlCode + RequestParameterConstants.JsonFormat + "?" +
                             RequestParameter.ApiKey(QuandlServiceConfiguration.ApiKey);

                if (_parameters.Options == null)
                {
                    return url;
                }
                return url + _parameters.Options.ToRequestParameter();
            }
        }
    }

    public class QuandlEntityRequest<T> : QuandlRequestV1 where T : QuandlEntity
    {
        public QuandlEntityRequest(T entity, OptionalRequestParameters options = null)
            : base(new RequestParameters {QuandlCode = entity.QuandlCode, Options = options})
        {
        }
    }
}